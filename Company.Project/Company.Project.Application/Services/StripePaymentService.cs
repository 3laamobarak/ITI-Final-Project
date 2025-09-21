using Stripe;
using Company.Project.Domain.Models;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Payment;
using Company.Project.Domain.Interfaces;
using StripeRefundService = Stripe.RefundService;
using DomainRefund = Company.Project.Domain.Models.Refund;
using StripeRefund = Stripe.Refund;
using static Company.Project.Domain.Enums.Enums;
using System.Linq.Expressions;

public class StripePaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StripePaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        StripeConfiguration.ApiKey = "sk_test_51RpsttIlJTDcsxVbNMSTD940JUXSRJhVVnQI5fuRBbcIDrTiJRd7zCQOnXJJpzsFQRBAGmXOwdn5578Teb0UFtbQ00j1TlZsVc"; 
    }

    public async Task<string> CreatePaymentIntentAsync(CreatePaymentDto dto, string userId)
    {
        Console.WriteLine($"[PAYMENT DEBUG] Starting payment creation for user: {userId}");
        Console.WriteLine($"[PAYMENT DEBUG] Cart items count: {dto.CartItems?.Count ?? 0}");
        Console.WriteLine($"[PAYMENT DEBUG] Amount: {dto.Amount}");
        
        // 1️⃣ إنشاء Order أولاً
        var order = new Order
        {
            UserId = userId,
            ShippingAddress = $"{dto.ShippingAddress}",
            Subtotal = dto.CartItems.Sum(i => i.Price * i.Quantity),
            Tax = 0,
            Discount = 0,
            ShippingCost = 4,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            OrderType = OrderType.Online,
            OrderItems = new List<OrderItem>()
        };
        
        Console.WriteLine($"[PAYMENT DEBUG] Order created with Total: {order.Total}");

        foreach (var item in dto.CartItems)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            if (product == null) throw new Exception($"Product with ID {item.ProductId} not found.");

            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Product = product
            });
        }

        await _unitOfWork.OrderRepository.AddAsync(order);
        await _unitOfWork.Completeasync();
        
        Console.WriteLine($"[PAYMENT DEBUG] Order saved to database with ID: {order.Id}");

        // 2️⃣ إنشاء PaymentIntent
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(order.Total * 100),
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" },
            Metadata = new Dictionary<string, string>
        {
            { "OrderId", order.Id.ToString() },
            { "UserId", userId }
        }
        };

        var service = new PaymentIntentService();
        var intent = await service.CreateAsync(options);

        // 3️⃣ إنشاء Payment
        var payment = new Payment
        {
            Amount = order.Total,
            PaymentMethod = Company.Project.Domain.Enums.Enums.PaymentMethod.CreditCard,
            IsSuccessful = false,
            OrderId = order.Id,
            UserId = userId,
            PaymentIntentId = intent.Id
        };

        await _unitOfWork.PaymentRepository.AddAsync(payment);
        await _unitOfWork.Completeasync();
        
        Console.WriteLine($"[PAYMENT DEBUG] Payment saved to database with ID: {payment.Id}, PaymentIntentId: {payment.PaymentIntentId}");

        return intent.ClientSecret;
    }

    public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
    {
        Console.WriteLine($"[PAYMENT DEBUG] Starting payment confirmation for PaymentIntentId: {paymentIntentId}");
        
        var service = new PaymentIntentService();
        var intent = await service.GetAsync(paymentIntentId);
        
        Console.WriteLine($"[PAYMENT DEBUG] Stripe payment status: {intent.Status}");

        var payment = await _unitOfWork.PaymentRepository.GetByPaymentIntentIdAsync(paymentIntentId);
        if (payment == null) 
        {
            Console.WriteLine($"[PAYMENT DEBUG] Payment not found in database for PaymentIntentId: {paymentIntentId}");
            throw new Exception("Payment not found");
        }
        
        Console.WriteLine($"[PAYMENT DEBUG] Found payment in database with ID: {payment.Id}, current IsSuccessful: {payment.IsSuccessful}");

        if (intent.Status == "succeeded")
        {
            payment.IsSuccessful = true;
            await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            await _unitOfWork.Completeasync();
            Console.WriteLine($"[PAYMENT DEBUG] Payment confirmed successfully in database");
            return true;
        }
        else
        {
            payment.IsSuccessful = false;
            await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            await _unitOfWork.Completeasync();
            Console.WriteLine($"[PAYMENT DEBUG] Payment marked as failed in database");
            return false;
        }
    }

    public async Task<List<PaymentDto>> GetAllPaymentsAsync()
    {
        var payments = await _unitOfWork.PaymentRepository.GetAllAsync(new Expression<Func<Payment, object>>[]
       {
    p => p.User,p => p.Order
       });

        return payments.Select(p => new PaymentDto
        {
            Id = p.Id,
            Amount = p.Amount,
            PaymentDate = p.PaymentDate,
            PaymentMethod = p.PaymentMethod.ToString(),
            IsSuccessful = p.IsSuccessful,
            OrderId = p.OrderId,
            UserId = p.UserId,
            PaymentIntentId = p.PaymentIntentId,
            RefundedAmount = p.RefundedAmount,
            UserName = p.User?.UserName,
            FullName = p.User != null ? $"{p.User.FirstName} {p.User.LastName}" : null,
            ShippingAddress = p.Order?.ShippingAddress
        }).ToList();
    }

    public async Task<bool> RefundPaymentAsync(int paymentId, decimal amount)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            throw new Exception("Payment not found.");
        if (!payment.IsSuccessful)
            throw new Exception("Cannot refund an unsuccessful payment.");
        if (string.IsNullOrEmpty(payment.PaymentIntentId))
            throw new Exception("PaymentIntentId is missing for this payment.");

        var refundOptions = new RefundCreateOptions
        {
            PaymentIntent = payment.PaymentIntentId,
            Amount = (long)(amount * 100)
        };

        var refundService = new StripeRefundService();
        StripeRefund stripeRefund;
        try
        {
            stripeRefund = await refundService.CreateAsync(refundOptions);
        }
        catch (StripeException sEx)
        {
            throw new Exception($"Stripe refund failed: {sEx.Message}");
        }

        if (stripeRefund == null)
            return false;

        if (string.Equals(stripeRefund.Status, "succeeded", StringComparison.OrdinalIgnoreCase)
            || string.Equals(stripeRefund.Status, "pending", StringComparison.OrdinalIgnoreCase))
        {
            var refundEntity = new DomainRefund
            {
                OrderId = payment.OrderId,
                //PaymentId = payment.Id,   
                Amount = amount,
                Reason = "Refund via Stripe",
                RequestDate = DateTime.UtcNow,
                ProcessedDate = DateTime.UtcNow,
                Status = RefundStatus.Completed
            };

            await _unitOfWork.RefundRepository.AddAsync(refundEntity);
            payment.RefundedAmount = payment.RefundedAmount + amount;
            await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            await _unitOfWork.Completeasync();
            return true;
        }

        return false;
    }
}

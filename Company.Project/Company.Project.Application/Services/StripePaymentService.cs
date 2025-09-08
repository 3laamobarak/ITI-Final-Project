using Stripe;
using Company.Project.Domain.Models;
using Company.Project.Application.Contracts;
using Company.Project.DTO.DTO.Payment;
using Company.Project.Domain.Interfaces;
using static Company.Project.Domain.Enums.Enums;

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

        return intent.ClientSecret;
    }

    public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
    {
        try
        {
            var service = new PaymentIntentService();
            var intent = await service.GetAsync(paymentIntentId);

            if (intent.Status == "succeeded")
            {
                var payment = await _unitOfWork.PaymentRepository
                    .GetByPaymentIntentIdAsync(paymentIntentId);

                if (payment != null)
                {
                    payment.IsSuccessful = true;
                    await _unitOfWork.PaymentRepository.UpdateAsync(payment);
                    await _unitOfWork.Completeasync();
                }
                return true;
            }

            return false;
        }
        catch (StripeException ex)
        {
            Console.WriteLine($"Stripe Error: {ex.Message}");
            throw new Exception("Failed to confirm payment.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
            throw;
        }
    }
}

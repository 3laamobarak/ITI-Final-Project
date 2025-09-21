using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.Infrastructure.Repositories;
using Company.Project.theDbcontext;
using Microsoft.AspNetCore.Identity;

namespace Company.Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private IBrandRepository _brandRepository;
        public ICartItemRepository _cartItemRepository;
        public ICategoryRepository _categoryRepository;
        public IMessageRepository _messageRepository;
        public IUserRepository _userRepository;
        


        private IExampleClassRepository _exampleClassRepository;
        public IProductRepository _productRepository;
        public IorderRepository _orderRepository;
        public IReviewRepository _reviewRepository;
        private IChatBotMessageRepository _chatBotMessagesRepository;

        public IBaseRepository<OTP> OTPs { get; private set; }
        private INutritionFactRepository _nutritionFactRepository;
        private IOrderItemRepository _orderItemRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IRefundRepository _refundRepository;
        public UnitOfWork(Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            OTPs = new BaseRepository<OTP>(_context);
            // _brandRepository = new BrandRepository<Brand>(_context);
            // _cartItemRepository = new CartItemRepository<CartItem>(_context);
            // _categoryRepository = new CategoryRepository<Category>(_context);
            // _exampleClassRepository = new ExampleClassRepository<ExampleClass>(_context);
            // _nutritionFactRepository = new NutritionFactRepository<NutritionFact>(_context);
            // _orderItemRepository = new OrderItemRepository<OrderItem>(_context);
            // _productCategoryRepository = new ProductCategoryRepository<ProductCategory>(_context);
            // _refundRepository = new RefundRepository<Refund>(_context);
            // _productRepository = new ProductRepository<Product>(_context);
            // _orderRepository = new OrderRepository<Order>(_context);
            // _reviewRepository = new ReviewRepository<Review>(_context);
            // _refundRepository = new RefundRepository<Refund>(_context);
        }

        public IExampleClassRepository ExampleClassRepository
        {
            get
            { return _exampleClassRepository ??= new ExampleClassRepository(_context); }
        }
        public ICartItemRepository CartItemRepository
        {
            get
            { return _cartItemRepository ??= new CartItemRepository(_context); }
        }
        public IProductRepository ProductRepository
        {
            get
            { return _productRepository ??= new ProductRepository(_context); }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            { return _categoryRepository ??= new CategoryRepository(_context); }
        }
        public IBrandRepository BrandRepository
        {
            get
            { return _brandRepository ??= new BrandRepository(_context); }
        }
        public IorderRepository OrderRepository
        {
            get
            { return _orderRepository ??= new OrderRepository(_context); }
        }
        public IReviewRepository ReviewRepository
        {
            get { return _reviewRepository ??= new ReviewRepository(_context); }
        }
        public INutritionFactRepository NutritionFactRepository
        {
            get { return _nutritionFactRepository ??= new NutritionFactRepository(_context); }
        }
        public IOrderItemRepository OrderItemRepository
        {
            get { return _orderItemRepository ??= new OrderItemRepository(_context); }
        }
        public IProductCategoryRepository ProductCategoryRepository
        {
            get { return _productCategoryRepository ??= new ProductCategoryRepository(_context); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_context, _userManager); }
        }


        public IRefundRepository RefundRepository
        {
            get { return _refundRepository ??= new RefundRepository(_context); }
        }


        public IChatBotMessageRepository ChatBotMessagesRepository
        {
            get { return _chatBotMessagesRepository ??= new ChatBotMessageRepository(_context); }
        }


        public async Task Completeasync()
        {
            await _context.SaveChangesAsync();
        }

        private IPaymentRepository _paymentRepository;
        public IPaymentRepository PaymentRepository
        {
            get { return _paymentRepository ??= new PaymentRepository(_context); }
        }
        public IMessageRepository ChatMessageRepository
        {
            get { return _messageRepository ??= new MessageRepository(_context); }
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

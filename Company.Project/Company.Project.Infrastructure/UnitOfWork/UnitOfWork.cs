using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.Infrastructure.Repositories;
using Company.Project.theDbcontext;

namespace Company.Project.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Context _context;

        //public IExampleClassRepository _exampleClassRepository;
        public ICartItemRepository _cartItemRepository;

        public IProductRepository _productRepository;

        public ICategoryRepository _categoryRepository;


        private IExampleClassRepository _exampleClassRepository;
        private IBrandRepository _brandRepository;

        public IorderRepository _orderRepository;
        public IReviewRepository _reviewRepository;
         
        public UnitOfWork(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IExampleClassRepository ExampleClassRepository
        {
            get
            {
                return _exampleClassRepository ??= new ExampleClassRepository(_context);
            }
        }

        public ICartItemRepository CartItemRepository
        {
            get
            {
                return _cartItemRepository ??= new CartItemRepository(_context);
            }
        }
             
        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }

        public IBrandRepository BrandRepository
        {
            get
            {
                return _brandRepository ??= new BrandRepository(_context);
            }
        }

        public IorderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new OrderRepository(_context);
            }
        }

        public IReviewRepository ReviewRepository
        {
            get { return _reviewRepository ??= new ReviewRepository(_context); }
        }
        public async Task Completeasync()
        {
            await _context.SaveChangesAsync();
        }


        public void Dispose()
        {
            _context.Dispose();
        }
        //public async Task<int> SaveChangesAsync()
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}

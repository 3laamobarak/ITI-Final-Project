    using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        IExampleClassRepository ExampleClassRepository { get; }
        ICartItemRepository CartItemRepository { get; }

        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }

        IorderRepository OrderRepository { get; }

        IUserRepository UserRepository { get; }

        IReviewRepository ReviewRepository { get; }
        IChatBotMessageRepository ChatBotMessagesRepository { get; }


        IPaymentRepository PaymentRepository { get; }
        INutritionFactRepository NutritionFactRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IRefundRepository RefundRepository { get; }
        Task Completeasync();
        void Dispose();
        //Task<int> SaveChangesAsync();
    }
}

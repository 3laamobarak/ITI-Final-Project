 using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IExampleClassRepository ExampleClassRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        Task Completeasync();
        void Dispose();
    }
}

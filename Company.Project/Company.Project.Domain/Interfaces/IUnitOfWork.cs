using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IExampleClassRepository ExampleClassRepository { get; }

        IorderRepository OrderRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task Completeasync();
        void Dispose();
    }
}

using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        IExampleClassRepository ExampleClassRepository { get; }

        IProductRepository ProductRepository { get; }

        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        Task Completeasync();
        void Dispose();
        //Task<int> SaveChangesAsync();
    }
}

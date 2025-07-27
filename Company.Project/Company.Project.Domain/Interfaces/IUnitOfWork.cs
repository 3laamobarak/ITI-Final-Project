using Company.Project.Domain.Models;

namespace Company.Project.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        IExampleClassRepository ExampleClassRepository { get; }
        IBrandRepository BrandRepository { get; }
        Task Completeasync();
        void Dispose();
        //Task<int> SaveChangesAsync();
    }
}

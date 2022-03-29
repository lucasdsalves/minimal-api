using demo_minimal_api.Models;

namespace demo_minimal_api.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAll();
        Task<Products> GetById(Guid id);
        Task<int> SaveNew(Products product);
        Task<bool> Update(Products product);
        Task<int> Delete(Guid id);
    }
}

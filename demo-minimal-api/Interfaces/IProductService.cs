using demo_minimal_api.ViewModels;

namespace demo_minimal_api.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
        Task<int> NewProduct(ProductViewModel product);
        Task<bool> UpdateProduct(ProductViewModel product);
        Task<int> DeleteProduct(Guid id);
    }
}

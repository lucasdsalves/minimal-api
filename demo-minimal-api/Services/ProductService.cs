using demo_minimal_api.Interfaces;
using demo_minimal_api.Models;
using demo_minimal_api.ViewModels;

namespace demo_minimal_api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var productList = await _productRepository.GetAll();

            return productList.Select(p => (ProductViewModel)p).OrderBy(p => p.Description).ToList();
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null) return null;

            return (ProductViewModel)product;
        }

        public async Task<int> NewProduct(ProductViewModel product)
        {
            return await _productRepository.SaveNew((Products)product); 
        }

        public async Task<bool> UpdateProduct(ProductViewModel product)
        {
            return await _productRepository.Update((Products)product);
        }

        public async Task<int> DeleteProduct(Guid id)
        {
            return await _productRepository.Delete(id);
        }
    }
}

using demo_minimal_api.ViewModels;

namespace demo_minimal_api.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public static explicit operator Products(ProductViewModel v)
        {
            return new Products
            {
                Id = v.Id,
                Code = v.Code,
                Description = v.Description,
                Price = v.Price,
                Active = v.Active,
            };
        }
    }
}

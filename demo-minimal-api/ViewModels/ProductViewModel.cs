using demo_minimal_api.Models;

namespace demo_minimal_api.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public static explicit operator ProductViewModel(Products v)
        {
            return new ProductViewModel
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

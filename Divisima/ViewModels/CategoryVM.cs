using DAL.Entities;

namespace Divisima.ViewModels
{
    public class CategoryVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

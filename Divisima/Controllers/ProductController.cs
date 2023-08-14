using BL.Repositories;
using DAL.Entities;
using Divisima.Models;
using Divisima.Tools;
using Divisima.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Divisima.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Category> repoCategory;
        IRepository<Brand> repoBrand;
        IRepository<ProductCategory> repoProductCategory;
        public ProductController(IRepository<Product> _repoProduct, IRepository<Category> _repoCategory, IRepository<Brand> _repoBrand, IRepository<ProductCategory> _repoProductCategory)
        {
            repoProduct = _repoProduct;
            repoCategory = _repoCategory;
            repoBrand = _repoBrand;
            repoProductCategory = _repoProductCategory;
        }

        [Route("/urunler")]
        [Route("/urunler/{name}-{catid}")]
        public IActionResult Index(string name,int? catid)
        {
            IEnumerable<Product> products= repoProduct.GetAll().Include(x=>x.ProductPictures);
            if(catid.HasValue)
            {
                if(repoProductCategory.GetAll(x=>x.CategoryID== catid).Any())
                {
                    int[] selectedProductIDs = repoProductCategory.GetAll(x => x.CategoryID == catid).Select(x => x.ProductID).ToArray(); //5,6,9
                    products= products.Where(x=> selectedProductIDs.Contains(x.ID));
                }
            }
            List<Category> categories = repoCategory.GetAll().Include(x => x.SubCategories).OrderBy(x => x.Name).ToList();
            foreach (Category category in categories) category.ProductCount = repoProductCategory.GetAll(x => x.CategoryID == category.ID).Count();
            IEnumerable<Brand> brands = repoBrand.GetAll().OrderBy(x => x.Name);
            CategoryVM categoryVM = new() { 
                Brands=brands,
                Categories=categories,
                Products=products
            };
            return View(categoryVM);
        }

        [Route("/detay/{name}-{id}")]
        public IActionResult Detail(string name,int id)
        {
            Product product = repoProduct.GetAll(x => x.ID == id).Include(x => x.ProductPictures).Include(x => x.Brand).FirstOrDefault();
            ProductVM productVM = new() {
                Product = product,
                RelatedProducts = repoProduct.GetAll(x => x.BrandID == product.BrandID && x.ID != product.ID).Include(x => x.ProductPictures).Include(x => x.Brand)
            };
            return View(productVM);
        }

        [Route("/urun/ara")]
        public IActionResult GetSearchProduct(string search)
        {
            return Json(repoProduct.GetAll(x=>x.Name.ToLower().Contains(search.ToLower()) || x.Description.ToLower().Contains(search.ToLower())).Include(i=>i.ProductPictures).Select(s=>new SearchProduct {Name=s.Name,Picture=s.ProductPictures.FirstOrDefault().Picture,Link="/detay/"+GeneralTool.URLConvert(s.Name)+"-"+s.ID }));
        }
    }
}

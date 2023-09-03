using BL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IRepository<Brand> _brandRepository;

        public HomeController(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        //[HttpGet]
        //public string getDate() { return DateTime.Now.ToString(); }

        [HttpGet]
        public IEnumerable<Brand> GetAllBrands()
        {
            return _brandRepository.GetAll();

            //VEYA AŞAĞISI GİBİ YAZILIR

            //return _brandRepository.GetAll(x => x.ID >= 10);

            //VEYA AŞAĞISI GİBİ MARKALARI İSME GÖRE SIRALAR

            return _brandRepository.GetAll(x => x.ID >= 10).OrderByDescending(X => X.Name);
        }

        [HttpGet("{id}")]
        public Brand GetBrandByID(int id)
        {
            return _brandRepository.GetBy(x => x.ID == id);
        }

        [HttpPost]
        public string AddBrand(Brand brand)
        {
            try
            {
                _brandRepository.Add(brand);
                //return brand.Name;

                //VEYA AŞAĞIDAKİ GİBİ DE YAZILABİLİNİR

                return brand.Name + " isimli marka başarılı bir şekilde eklendi.";
            }
            catch (Exception exception)
            {

                return "Bir hata meydana geldi. Detay:" + exception.Message;
            }
        }

        [HttpPost("{id}")]
        public string DeleteBrand(int id)
        {
            try
            {
                Brand deletedBrand = _brandRepository.GetBy(x => x.ID == id);
                _brandRepository.Delete(deletedBrand);

                return deletedBrand.Name + " isimli marka başarılı bir şekilde silindi.";
            }
            catch (Exception exception)
            {
                return "Bir hata meydana geldi. Detay:" + exception.Message;
            }
        }
    }
}
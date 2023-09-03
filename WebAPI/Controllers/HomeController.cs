using BL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            //return _brandRepository.GetAll(x => x.ID >= 10).OrderByDescending(X => X.Name);
        }





        [HttpGet("{id}")]
        public Brand GetBrandByID(int id)
        {
            return _brandRepository.GetBy(x => x.ID == id);
        }




        //<summary> SWAGGER İÇERİSİNDE GÖZÜKEN METOTLARIN YANLARINDA YAZMIŞ OLDUĞUMUZ AÇIKLAMALARINI GÖSTERİYOR
        //AŞAĞIDAKİ KISMI KULLANABİLMEK İÇİN WebAPI ÜZERİNE GELİP SAĞ TIKLIYORUZ => PROPERTIES => BUILD => OUTPUT => DOCUMENTATION FILE KISMINI CHECK YAPIYORUZ 
        /// <summary>
        /// Bu metodu kullanırken lütfen parametre girdisi 30 karakteri geçmesin
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
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




        //<summary> SWAGGER İÇERİSİNDE GÖZÜKEN METOTLARIN YANLARINDA YAZMIŞ OLDUĞUMUZ AÇIKLAMALARINI GÖSTERİYOR
        //AŞAĞIDAKİ KISMI KULLANABİLMEK İÇİN WebAPI ÜZERİNE GELİP SAĞ TIKLIYORUZ => PROPERTIES => BUILD => OUTPUT => DOCUMENTATION FILE KISMINI CHECK YAPIYORUZ 
        /// <summary>
        /// Gönderilecek olan parametreler tam sayı türünde olmalıdır
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
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

        [HttpPut]
        public string DeleteBrand(Brand brand)
        {
            try
            {
                _brandRepository.Update(brand);

                return brand.Name + " isimli marka başarılı bir şekilde güncellendi.";

            }
            catch (Exception exception)
            {

                return "Bir hata meydana geldi. Detay:" + exception.Message;
            }
        }
    }
}
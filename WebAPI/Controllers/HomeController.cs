using BL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private IRepository<Brand> _brandRepository;
        private IRepository<Admin> _adminRepository;

        public HomeController(IRepository<Brand> brandRepository, IRepository<Admin> adminRepository)
        {
            _brandRepository = brandRepository;
            _adminRepository = adminRepository;
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
        public string UpdateBrand(Brand brand)
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



        //----------- LOGİN İŞLEMLERİ ---------------


        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/login")]
        public string Login(string username, string password)
        {
            string md5Password = getMD5(password);

            var admin = _adminRepository.GetBy(x => x.Username == username && x.Password == md5Password); //PASSWORD'Ü MD5 LENMİŞ PASSWORD İLE EŞLEŞTİRİYORUZ ÇÜNKÜ KAYIT OLURKEN VERİ TABANINA KAYDEDİLEN PASSWORD MD5LENMİŞ ŞEKİLDE KAYIT EDİLİYOR.

            if (admin != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.PrimarySid,admin.ID.ToString()),
                    new Claim($"{username}",admin.Name),
                    new Claim(ClaimTypes.Name,admin.Name+" "+admin.Surname)
                };

                string signInKey = "benimözelkeybilgisi";
                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey));
                SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                //ALT SATIRLARDA TOKEN OLUŞTURMA YAPILIYOR
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                    (
                        issuer: "http://localhost:5174", //TOKEN SAĞLAYICI URL
                        audience: "trendyol", //KİMLİĞİ KULLANACAK OLAN FİRMA VEYA UYGULAMA
                        claims: claims,
                        expires: DateTime.Now.AddDays(14), //TOKEN GEÇERLİLİK SÜRESİ
                        notBefore: DateTime.Now, //TOKEN GEÇERLİLİĞİ NE ZAMAN BAŞLASIN
                        signingCredentials: signingCredentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            else return "Geçersiz kullanıcı adı veya şifre";
        }
    }
}
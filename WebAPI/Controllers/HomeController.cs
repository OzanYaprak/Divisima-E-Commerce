using BL.Repositories;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        IRepository<Brand> repoBrand;
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Brand> _repoBrand, IRepository<Admin> _repoAdmin)
        {
            repoBrand = _repoBrand;
            repoAdmin = _repoAdmin;
        }
        //[HttpGet]
        //public string getDate()
        //{
        //    return DateTime.Now.ToString();
        //}

        [HttpGet]
        public IEnumerable<Brand> GetBrands() { 
            //return repoBrand.GetAll(x=>x.ID>=10).OrderByDescending(x=>x.Name);
            return repoBrand.GetAll().OrderByDescending(x=>x.Name);
        }

        [HttpGet("{id}")]
        public Brand GetBrand(int id)
        {
            return repoBrand.GetBy(x => x.ID == id);
        }
        /// <summary>
        /// Bu metodu kullanırken lütfen 30 karakter geçmesin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Add(Brand model)
        {
            try
            {
                repoBrand.Add(model);
                return model.Name + " isimli marka başarıyla eklendi...";
            }
            catch (Exception ex)
            {
                return "Bir hata oluştu, açıklama: " + ex.Message;
            }
        }
        /// <summary>
        /// Gönderilecek olan parametre tam sayı türünde olmalıdır
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public string Delete(int id) {
            try
            {
                Brand brand = repoBrand.GetBy(x => x.ID == id);
                repoBrand.Delete(brand);
                return brand.Name + " isimli marka başarıyla silindi...";
            }
            catch (Exception ex)
            {
                return "Bir hata oluştu, açıklama: " + ex.Message;
            }
        }

        [HttpPut]
        public string Update(Brand model) {
            try
            {
                repoBrand.Update(model);
                return model.Name + "isimli marka başarıyla güncellendi...";
            }
            catch (Exception ex)
            {
                return "Bir hata oluştu, açıklama: " + ex.Message;
            }
        }

        [AllowAnonymous,Route("/api/login"),HttpGet]
        public string Login(string username,string password)
        {
            string md5Password = getMD5(password);
            Admin admin = repoAdmin.GetBy(x => x.Username == username && x.Password == md5Password) ?? null;
            if (admin != null)
            {
                List<Claim> claims = new List<Claim>() { 
                    new Claim(ClaimTypes.PrimarySid,admin.ID.ToString()),
                    new Claim(ClaimTypes.Name,admin.Name+" "+admin.Surname)
                };
                string signinKey = "benimözelkeybilgisi";
                SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(signinKey));
                SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken jwtSecurityToken = new(
                    issuer: "http://localhost:5174",//token sağlayıcı url
                    audience:"n11",//kimliği kullanacak olan firma veya uygulama adı
                    claims: claims,
                    expires:DateTime.Now.AddDays(10),//token geçerlilik süresi
                    notBefore:DateTime.Now,//geçerliliği ne zaman başlasın
                    signingCredentials: signingCredentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            else return "Geçersiz kullanıcı adı veya şifre";
        }

        public static string getMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}

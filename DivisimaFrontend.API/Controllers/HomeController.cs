using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace DivisimaFrontend.API.Controllers
{
    public class HomeController : Controller
    {
        //--------API DEN GELEN BİLGİLERİ DESERIALIZE EDIP CONTROLLERDA ISLEME---------
        public IActionResult Index()
        {
            var httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://localhost:5174/");

            var result = httpclient.GetAsync("api/home").Result; //METOT ADINI YAZIYORUZ

            var jsonResult = result.Content.ReadAsStringAsync().Result; //GELEN DATAYI STRING OLARAK OKUYOR/DÖNÜYOR

            //JSON FORMATINDA STRING OLARAK GELEN jsonResult KISMINI IEnumerable<Brand> E DÖNÜŞTÜRÜYORUZ
            IEnumerable<Brand> brandList = JsonSerializer.Deserialize<IEnumerable<Brand>>(jsonResult, new JsonSerializerOptions(JsonSerializerDefaults.Web)); //...Web)).Where(x=>x.Name.Contains("abc") BU ŞEKİLDE SONUNA EKLEME YAPARAK LİNQ SORGULARIDA YAZABİLİRİZ.

            //YUKARIDAKİ KISMI IEnumerable<Brand> brandList = JsonSerializer.Deserialize<IEnumerable<Brand>>(jsonResult); BU ŞEKİLDE YAZIDĞIMIZ ZAMAN DESERIALIZE EDILEN BRANDLER İÇERİSİNDEKİ PROPERTYLER KÜÇÜK HARFLER İLE YAZILMIŞ ŞEKİLDE GELİYOR FAKAT,
            //BİZİM ENTITYMIZ ICERISINDE BIZ PROPERTYLERIN BAZI HARFLERINI BUYUK YAZIYORUZ, BU ŞEKİLDE UYUŞMADIĞINDAN YUKARIDA YAZAN brandList İÇİN NULL OLARAK GELİYOR, BUNUN YERİNE KODUN İLGİLİ KISMINA AŞAĞIDAKİ EKLERİ YAPIYORUZ,
            //(jsonResult,new JsonSerializerOptions(JsonSerializerDefaults.Web))

            return View(brandList);
        }



        [Route("yenimarkaekle")]
        public IActionResult AddNewBrand(string name)
        {
            var httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://localhost:5174/");

            var newBrand = new Brand() { Name = name };

            string serializeBrand = JsonSerializer.Serialize(newBrand); //YENİ EKLENEN BRAND'İ BU SATIRDA STRING E YANİ JSON FORMATINA DÖNÜŞTÜRÜYORUZ.

            StringContent stringContent = new StringContent(serializeBrand, Encoding.UTF8, "application/json"); // JSON FORMATINA DÖNÜŞTÜRÜLEN serializeBrand İÇİNDE Encoding.UTF8 İLE KARAKTERLERE DİKKAT ET, "application/json" FORMATINDA DA DÜZENLE.

            var result = httpclient.PostAsync("api/home", stringContent).Result; //BU SATIRDA stringContent KISMINDA DÜZENLENMİŞ JSON FORMATINI POST EDİYORUZ.

            return RedirectToAction("Index");
        }
    }
}

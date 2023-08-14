using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Divisima.CorporateWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("http://localhost:5174/");
            var result = httpclient.GetAsync("api/home").Result;
            var jsonResult=result.Content.ReadAsStringAsync().Result;
            IEnumerable<Brand> brands = JsonSerializer.Deserialize<IEnumerable<Brand>>(jsonResult, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            //var contentThreeDPayment = new StringContent(JsonConvert.SerializeObject(new
            //{
            //    x=y
            //    clientId = Convert.ToInt64(_clientId),
            //    apiUser = _apiUser,
            //    rnd = rnd,
            //    timeSpan = timeSpan,
            //    hash = CreateHash(_apiPass, _clientId, _apiUser, rnd, timeSpan),
            //    callbackUrl = _returnUrl,
            //    orderId = _orderNumber,
            //    amount = _price,
            //    currency = 949,
            //    installmentCount = _instalment > 1 ? _instalment : 0
            //}), Encoding.UTF8, "application/json");

            return View(brands);
        }

        [Route("/yenimarka")]
        public IActionResult NewBrand(string name)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5174/");
            Brand brand = new() { Name= name };
            string serializeBrand= JsonSerializer.Serialize(brand);
            StringContent stringContent = new(serializeBrand, Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync("api/home", stringContent).Result;
            return Redirect("/");
        }
    }
}

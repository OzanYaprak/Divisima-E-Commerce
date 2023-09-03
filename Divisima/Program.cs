using BL.Repositories;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/admin/giris";
    opt.LogoutPath = "/admin/cikis";
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});
var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseStatusCodePagesWithRedirects("/hata/{0}");

//1.MIDDLEWARE ÖRNEÐÝ AÞAÐIDAKÝ GÝBÝDÝR, BU KOD SATIRLARI YAZILDIÐINDA PROJE ALT TARAFLARA GEÇMEDEN (SAYFAYA ÝLK ÝSTEKTE BULNULDUÐUNDA) ARAYA GÝREREK ALT TARAFTAKÝ MIDDLEWARE'Ý ÇALIÞTIRACAK.
//app.Run(async context => context.Response.Redirect("https://www.google.com.tr"));




//EÐER AÞAÐIDAKÝ GÝBÝ BÝR MIDDLEWARE YAZARSAK PROJEYE ÝLK ÝSTEKTE BULUNULDUÐUNDA NORMAL ANA SAYFA AÇILIR FAKAT ACTÝON OLARAK /test YAZARSAK MÝDDLEWARE ÝÇERÝSÝNDEKÝ URL YE YÖNLENDÝRME YAPILIR.
//app.Map("/test", app =>
//    app.Run(async context =>
//    {
//         context.Response.Redirect("https://www.google.com.tr");
//    }));

//DÝÐER BÝR YÖNTEMÝ

//app.Use(async (context, next) =>
//{
//    var url = context.Request.Path.Value;
//    if (url.Contains("/test"))
//    {
//        context.Response.Redirect("https://www.google.com.tr");
//        return;
//    }
//    await next();
//});


//1.MIDDLEWARE ÖRNEÐÝNE ÇOK BENZER YAPIDA BULUNUR FAKAT AÞAÐIDA GÖZÜKTÜÐÜ GÝBÝ MAPWHEN KISMINDA ÝÇERÝSÝNE BÝRÇOK ÞART YAZILABÝLÝNÝR
//app.MapWhen(x => x.Request.Method == "GET" && x.Request.Path.Value.Contains("/abc") && !x.Request.IsHttps, app =>
//{
//    app.Run(async context => await context.Response.WriteAsync("Middleware 2"));
//});



app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();//kimlik doðrulama
app.UseAuthorization();//kimlik yetkilendirme
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.Run();
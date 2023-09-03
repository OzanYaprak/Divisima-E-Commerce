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

//1.MIDDLEWARE �RNE�� A�A�IDAK� G�B�D�R, BU KOD SATIRLARI YAZILDI�INDA PROJE ALT TARAFLARA GE�MEDEN (SAYFAYA �LK �STEKTE BULNULDU�UNDA) ARAYA G�REREK ALT TARAFTAK� MIDDLEWARE'� �ALI�TIRACAK.
//app.Run(async context => context.Response.Redirect("https://www.google.com.tr"));




//E�ER A�A�IDAK� G�B� B�R MIDDLEWARE YAZARSAK PROJEYE �LK �STEKTE BULUNULDU�UNDA NORMAL ANA SAYFA A�ILIR FAKAT ACT�ON OLARAK /test YAZARSAK M�DDLEWARE ��ER�S�NDEK� URL YE Y�NLEND�RME YAPILIR.
//app.Map("/test", app =>
//    app.Run(async context =>
//    {
//         context.Response.Redirect("https://www.google.com.tr");
//    }));

//D��ER B�R Y�NTEM�

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


//1.MIDDLEWARE �RNE��NE �OK BENZER YAPIDA BULUNUR FAKAT A�A�IDA G�Z�KT��� G�B� MAPWHEN KISMINDA ��ER�S�NE B�R�OK �ART YAZILAB�L�N�R
//app.MapWhen(x => x.Request.Method == "GET" && x.Request.Path.Value.Contains("/abc") && !x.Request.IsHttps, app =>
//{
//    app.Run(async context => await context.Response.WriteAsync("Middleware 2"));
//});



app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();//kimlik do�rulama
app.UseAuthorization();//kimlik yetkilendirme
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.Run();
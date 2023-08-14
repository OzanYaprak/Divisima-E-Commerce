using BL.Repositories;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddDbContext<SqlContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/admin/giris";
    opt.LogoutPath = "/admin/cikis";
    opt.ExpireTimeSpan= TimeSpan.FromMinutes(60);    
});
var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseStatusCodePagesWithRedirects("/hata/{0}");
//app.Run(async context => await context.Response.WriteAsync("selam"));

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware 1");

//    await next.Invoke();

//    await context.Response.WriteAsync("Middleware 2");
//});

//app.Map("/test", app =>
//    app.Run(async context =>
//    {
//        await context.Response.WriteAsync("Middleware 1");
//    }));

//app.Use(async (context, next) =>
//{
//    var url = context.Request.Path.Value;
//    if (url.Contains("/infotech"))
//    {
//        context.Response.Redirect("https://www.infotechacademy.com.tr");
//        return;
//    }
//    await next();
//});

//app.MapWhen(x => x.Request.Method == "GET" && !x.Request.IsHttps && x.Request.Path.Value.Contains("/abc"), app =>
//{
//    app.Run(async context => await context.Response.WriteAsync("Middleware 2"));
//});



app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();//kimlik doðrulama
app.UseAuthorization();//kimlik yetkilendirme
app.MapControllerRoute(name:"default",pattern:"{controller=home}/{action=index}/{id?}");
app.MapControllerRoute(name:"area",pattern:"{area:exists}/{controller=home}/{action=index}/{id?}");
app.Run();
using BL.Repositories;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new() {
        ValidateIssuer = true,//token saðlayýcý url'in doðrulanmasý
        ValidateAudience = true,//kimliði kullanacak olan firma adý doðrulamasý
        ValidateLifetime = true,//token geçerlilik süresi doðrulamasý
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5174",//token saðlayýcý url
        ValidAudience = "n11",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benimözelkeybilgisi"))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Divisima API - Project",
        Description="Bu proje .net Core 7 ile yazýlmýþtýr.",
        TermsOfService=new Uri("https://www.infotechacademy.com.tr/kurumsal/insan-kaynaklari-politikasi"),
        Contact=new OpenApiContact
        {
            Name="Özgür Bey",
            Email="ozgur@gmail.com"
        },
        License=new OpenApiLicense
        {
            Name="MIT License",
            Url= new Uri("https://www.infotechacademy.com.tr/kurumsal/hakkimizda"),
        }
    });
    sw.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,Assembly.GetExecutingAssembly().GetName().Name+".xml"));
});
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy(name: "izinVerilenOriginler", policy =>
//    {
//        policy.WithOrigins("http://localhost:5002").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//    });
//});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors("izinVerilenOriginler");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

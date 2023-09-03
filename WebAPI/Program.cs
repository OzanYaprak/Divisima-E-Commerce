using BL.Repositories;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // UI TARAFININ AKS�NE BURADA AddControllersWithViews YER�NE SADECE AddControllers YAZIYOR, ��NK� API TARAFINDA VIEW ILE YAPILACAK B�R ��LEM OLMUYOR GENELL�KLE DATA �ZER�NDEN ��LEM YAPILIYOR.

builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));

builder.Services.AddEndpointsApiExplorer();

//A�A�IDA SWAGGER D�ZENLEMES� ���N D�ZENLENM�� OLAN KODLAR BULUNMAKTADIR ORJ�NAL HAL� " builder.Services.AddSwaggerGen(); " �EKL�NDED�R. FARKI BURADAN ANLAYAB�L�RS�N.
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "�ZEL API YAPILANDIRILMASI", //SWAGGER KISMINDA YAZAN T�TLE KISMINI DE���T�R�YOR.
        Description = "Bu proje e�itim ama�l� haz�rlanm��t�r",
        TermsOfService = new Uri("https://policies.google.com/terms?hl=tr"), //KULLANIM S�ZLE�MES�.
        Contact = new OpenApiContact { Name="OzanYaprak", Email="oznyprk@gmail.com", Url= new Uri ("https://github.com/OzanYaprak") },
        License = new OpenApiLicense { Name= "MIT License", Url= new Uri("https://policies.google.com/terms?hl=tr") }
    });
    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml")); //<summary> KISMINI KULLANAB�LMEM�Z ���N YAZMAMIZ GEREKEN SATIR
});


//CORS POLICY ICIN YAZILDI " FRONTEND KISMINA BILGI CEKMEK ICIN YAZILMASI GEREKIYOR " 
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("izinVerilenOriginler", policy =>
    {
        policy.WithOrigins("http://localhost:5072").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS POLICY ICIN YAZILDI " FRONTEND KISMINA BILGI CEKMEK ICIN YAZILMASI GEREKIYOR " 
app.UseCors("izinVerilenOriginler");

app.UseAuthorization();

app.MapControllers();

app.Run();

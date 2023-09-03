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



//JWT ���N YAZILDI --- BA�LANGI�
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //TOKEN SA�LAYICI URL'N�N DO�RULANMASI
        ValidateAudience = true, //K�ML��� KULLANACAK OLAN F�RMA ADININ DO�RULANMASI
        ValidateLifetime = true, //TOKEN GE�ERL�L�K S�RES� DO�RULAMASI
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5174", //TOKEN SA�LAYICI URL
        ValidAudience = "trendyol", //TOKEN K�ME VER�L�YOR
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benim�zelkeybilgisi"))
    };
});




builder.Services.AddEndpointsApiExplorer();

//A�A�IDA SWAGGER FRONTEND D�ZENLEMES� ���N D�ZENLENM�� OLAN KODLAR BULUNMAKTADIR ORJ�NAL HAL� " builder.Services.AddSwaggerGen(); " �EKL�NDED�R. FARKI BURADAN ANLAYAB�L�RS�N.
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "�ZEL API YAPILANDIRILMASI", //SWAGGER KISMINDA YAZAN T�TLE KISMINI DE���T�R�YOR.
        Description = "Bu proje e�itim ama�l� haz�rlanm��t�r",
        TermsOfService = new Uri("https://policies.google.com/terms?hl=tr"), //KULLANIM S�ZLE�MES�.
        Contact = new OpenApiContact { Name = "OzanYaprak", Email = "oznyprk@gmail.com", Url = new Uri("https://github.com/OzanYaprak") },
        License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://policies.google.com/terms?hl=tr") }
    });
    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml")); //<summary> KISMINI KULLANAB�LMEM�Z ���N YAZMAMIZ GEREKEN SATIR
});


//CORS POLICY ICIN YAZILDI "AJAX �LE YAN� JAVASCRIPT �LE FRONTEND KISMINA BILGI CEKMEK �STEN�YORSA YAZILMASI GEREKIYOR " 
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("izinVerilenOriginler", policy =>
//    {
//        policy.WithOrigins("http://localhost:5072").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//    });
//}); //BURAYI EN SON CONTROLLLERDAN B�LG� �EKT���M�Z ���N PAS�F HALE GET�RD�M.



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS POLICY ICIN YAZILDI " FRONTEND KISMINA BILGI CEKMEK ICIN YAZILMASI GEREKIYOR " 
//app.UseCors("izinVerilenOriginler");      //BURAYI EN SON CONTROLLLERDAN B�LG� �EKT���M�Z ���N PAS�F HALE GET�RD�M.

app.UseAuthentication(); //JWT ���N YAZILDI -- BURAYI YAZDIKTAN SONRA NUGET ��ER�S�NDEN Microsoft.AspNetCore.Authentication.Jwt.Bearer ADINDA B�R PAKET� Y�KLEMEM�Z GEREK�YOR.

app.UseAuthorization();

app.MapControllers();

app.Run();

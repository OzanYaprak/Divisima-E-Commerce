using BL.Repositories;
using DAL.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // UI TARAFININ AKSÝNE BURADA AddControllersWithViews YERÝNE SADECE AddControllers YAZIYOR, ÇÜNKÜ API TARAFINDA VIEW ILE YAPILACAK BÝR ÝÞLEM OLMUYOR GENELLÝKLE DATA ÜZERÝNDEN ÝÞLEM YAPILIYOR.

builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
builder.Services.AddDbContext<SqlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CS1")));



//JWT ÝÇÝN YAZILDI --- BAÞLANGIÇ
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //TOKEN SAÐLAYICI URL'NÝN DOÐRULANMASI
        ValidateAudience = true, //KÝMLÝÐÝ KULLANACAK OLAN FÝRMA ADININ DOÐRULANMASI
        ValidateLifetime = true, //TOKEN GEÇERLÝLÝK SÜRESÝ DOÐRULAMASI
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5174", //TOKEN SAÐLAYICI URL
        ValidAudience = "trendyol", //TOKEN KÝME VERÝLÝYOR
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benimözelkeybilgisi"))
    };
});




builder.Services.AddEndpointsApiExplorer();

//AÞAÐIDA SWAGGER FRONTEND DÜZENLEMESÝ ÝÇÝN DÜZENLENMÝÞ OLAN KODLAR BULUNMAKTADIR ORJÝNAL HALÝ " builder.Services.AddSwaggerGen(); " ÞEKLÝNDEDÝR. FARKI BURADAN ANLAYABÝLÝRSÝN.
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ÖZEL API YAPILANDIRILMASI", //SWAGGER KISMINDA YAZAN TÝTLE KISMINI DEÐÝÞTÝRÝYOR.
        Description = "Bu proje eðitim amaçlý hazýrlanmýþtýr",
        TermsOfService = new Uri("https://policies.google.com/terms?hl=tr"), //KULLANIM SÖZLEÞMESÝ.
        Contact = new OpenApiContact { Name = "OzanYaprak", Email = "oznyprk@gmail.com", Url = new Uri("https://github.com/OzanYaprak") },
        License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://policies.google.com/terms?hl=tr") }
    });
    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml")); //<summary> KISMINI KULLANABÝLMEMÝZ ÝÇÝN YAZMAMIZ GEREKEN SATIR
});


//CORS POLICY ICIN YAZILDI "AJAX ÝLE YANÝ JAVASCRIPT ÝLE FRONTEND KISMINA BILGI CEKMEK ÝSTENÝYORSA YAZILMASI GEREKIYOR " 
//builder.Services.AddCors(opt =>
//{
//    opt.AddPolicy("izinVerilenOriginler", policy =>
//    {
//        policy.WithOrigins("http://localhost:5072").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//    });
//}); //BURAYI EN SON CONTROLLLERDAN BÝLGÝ ÇEKTÝÐÝMÝZ ÝÇÝN PASÝF HALE GETÝRDÝM.



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CORS POLICY ICIN YAZILDI " FRONTEND KISMINA BILGI CEKMEK ICIN YAZILMASI GEREKIYOR " 
//app.UseCors("izinVerilenOriginler");      //BURAYI EN SON CONTROLLLERDAN BÝLGÝ ÇEKTÝÐÝMÝZ ÝÇÝN PASÝF HALE GETÝRDÝM.

app.UseAuthentication(); //JWT ÝÇÝN YAZILDI -- BURAYI YAZDIKTAN SONRA NUGET ÝÇERÝSÝNDEN Microsoft.AspNetCore.Authentication.Jwt.Bearer ADINDA BÝR PAKETÝ YÜKLEMEMÝZ GEREKÝYOR.

app.UseAuthorization();

app.MapControllers();

app.Run();

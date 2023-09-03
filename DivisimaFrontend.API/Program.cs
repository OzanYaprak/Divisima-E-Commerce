namespace DivisimaFrontend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorPages(); //ÝPTAL EDÝLDÝ
            builder.Services.AddControllersWithViews(); // YERÝNE EKLENDÝ (CONTROLLER VE VIEW YAPINSINDA ÇALIÞABÝLMEK ÝÇÝN)

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapRazorPages(); //ÝPTAL EDÝLDÝ

            app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}"); //EKLENDÝ (CONTROLLER ROUTE YAPISINDA ÇALIÞABÝLMEK ÝÇÝN)

            app.Run();
        }
    }
}
namespace DivisimaFrontend.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorPages(); //�PTAL ED�LD�
            builder.Services.AddControllersWithViews(); // YER�NE EKLEND� (CONTROLLER VE VIEW YAPINSINDA �ALI�AB�LMEK ���N)

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapRazorPages(); //�PTAL ED�LD�

            app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}"); //EKLEND� (CONTROLLER ROUTE YAPISINDA �ALI�AB�LMEK ���N)

            app.Run();
        }
    }
}
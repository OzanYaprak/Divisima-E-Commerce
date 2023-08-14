using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Contexts
{
    public class SqlContext:DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> opt):base(opt)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

            modelBuilder.Entity<Product>().HasOne(x=>x.Brand).WithMany(x=>x.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.ProductID, x.CategoryID });

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                ID=1,
                Name="Bekir",
                Surname="Oturakçı",
                Username="bekir",
                Password= "202cb962ac59075b964b07152d234b70"
            });
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
    }
}

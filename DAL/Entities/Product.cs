using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Product")]
    public class Product
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Ürün Adı")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Satış Fiyat"), Required(ErrorMessage = "Satış Fiyatı  gerekli")]
        public decimal Price { get; set; }

        [Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Ürün Açıklaması")]
        public string Description { get; set; }

        [Column(TypeName = "text"), Display(Name = "Ürün Detayı")]
        public string Detail { get; set; }

        [Display(Name = "Stok Sayısı"), Required(ErrorMessage = "Stok Sayısı gerekli")]
        public int Stock { get; set; }

        [Display(Name = "Görüntülenme Sırası"),Required(ErrorMessage = "Görüntülenme Sırası gerekli")]
        public int DisplayIndex { get; set; }

        [Display(Name = "Marka")]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductPicture> ProductPictures { get; set; }

        [Display(Name = "Ürün Kategorileri")]
        public ICollection<ProductCategory> ProductCategories { get; set; }

        [Display(Name = "Siparis Detayları")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

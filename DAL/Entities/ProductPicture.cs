using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("ProductPicture")]
    public class ProductPicture
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Resim Adı")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Ürün Resmi")]
        public string Picture { get; set; }

        [Display(Name = "Bağlı Olduğu Ürün")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}

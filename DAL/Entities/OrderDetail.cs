using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int ID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        [Display(Name = "Ürün")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Ürün Adı")]
        public string ProductName { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Ürün Resmi")]
        public string ProductPicture { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Ürün Fiyatı")]
        public decimal ProductPrice { get; set; }
                
        [Display(Name = "Miktar")]
        public int Quantity { get; set; }
    }
}

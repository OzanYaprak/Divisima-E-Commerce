using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory
    {
        [Display(Name ="Ürün")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}

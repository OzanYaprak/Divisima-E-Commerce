using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Category")]
    public class Category
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Kategori Adı")]
        public string Name { get; set; }

        [Display(Name = "Üst Kategori Adı")]
        public int? ParentID { get; set; }
        public Category ParentCategory { get; set; }

        [Display(Name = "Alt Kategoriler")]
        public ICollection<Category> SubCategories { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        [NotMapped]
        public int ProductCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Slide")]
    public class Slide
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(50)"),StringLength(50),Display(Name ="Slayt Adı"),Required(ErrorMessage ="Slayt Adı Boş Geçilemez")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(100)"), StringLength(100), Display(Name = "Slayt Başlığı")]
        public string Title { get; set; }

        [Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Slayt Açıklaması")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Slayt Resmi")]
        public string Picture { get; set; }

        [Column(TypeName = "decimal(18,2)"), Display(Name = "Slayt Fiyat"), Required(ErrorMessage = "Slayt Fiyatı Boş Geçilemez")]
        public decimal Price { get; set; }

        [Column(TypeName = "varchar(150)"), StringLength(150), Display(Name = "Slayt Linki")]
        public string Link { get; set; }

        [Display(Name = "Görüntülenme Sırası"), Required(ErrorMessage = "Slayt Sırası Boş Geçilemez")]
        public int DisplayIndex { get; set; }
    }
}

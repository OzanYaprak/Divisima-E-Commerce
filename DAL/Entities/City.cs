using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("City")]
    public class City
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Şehir Adı"), Required(ErrorMessage = "Şehir Adı Boş Geçilemez")]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}

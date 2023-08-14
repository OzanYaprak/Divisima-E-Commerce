using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("District")]
    public class District
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "İlçe Adı"), Required(ErrorMessage = "İlçe Adı Boş Geçilemez")]
        public string Name { get; set; }

        public int CityID { get; set; }
        public City City { get; set; }
    }
}

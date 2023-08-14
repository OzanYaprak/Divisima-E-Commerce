using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Admin")]
    public class Admin
    {
        public int ID { get; set; }

        [Column(TypeName ="varchar(30)"),StringLength(30),Display(Name ="Admin Adı")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(30)"), StringLength(30), Display(Name = "Admin Soyadı")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Kullanıcı Adı"),Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez")]
        public string Username { get; set; }

        [Column(TypeName = "varchar(32)"), StringLength(32), Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string Password { get; set; }

        [Display(Name = "Son Giriş Yaptığı Tarih / Saat")]
        public DateTime LastLoginDate { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20), Display(Name = "Son Giriş Yaptığı IP NO")]
        public string LastLoginIPNo { get; set; }
    }
}

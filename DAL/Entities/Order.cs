using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Order")]
    public class Order
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(10)"), StringLength(10), Display(Name = "Sipariş Numarası"), Required(ErrorMessage = "Slayt Adı Boş Geçilemez")]
        public string OrderNumber { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "Şehir")]
        public string City { get; set; }

        [Column(TypeName = "varchar(50)"), StringLength(50), Display(Name = "İlçe")]
        public string District { get; set; }

        [Column(TypeName = "varchar(250)"), StringLength(250), Display(Name = "Adres")]
        public string Address { get; set; }

        [Column(TypeName = "char(5)"), StringLength(5), Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }

        [Display(Name = "Ödeme Türü")]
        public EPaymentType PaymentType { get; set; }
        
        [Display(Name = "Sipariş Durumu")]
        public EOrderStatus OrderStatus { get; set; }

        [Display(Name = "Sipariş Tarihi")]
        public DateTime RecDate { get; set; }

        [Column(TypeName = "varchar(20)"), StringLength(20),Display(Name = "Siparişi Veren IP NO")]
        public string IP { get; set; }

        [Column(TypeName = "varchar(100)"), StringLength(100),Display(Name = "Adı Soyadı")]
        public string NameSurname { get; set; }

        [Column(TypeName = "varchar(100)"), StringLength(100),Display(Name = "Mail Adresi")]
        public string MailAddress { get; set; }

        [Column(TypeName = "varchar(30)"), StringLength(30), Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string CardOwner { get; set; }

        [NotMapped]
        public string CardNumber { get; set; }

        [NotMapped]
        public string Month { get; set; }

        [NotMapped]
        public string Year { get; set; }

        [NotMapped]
        public string CVV { get; set; }
    }
}

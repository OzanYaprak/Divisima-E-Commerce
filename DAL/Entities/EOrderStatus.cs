using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public enum EOrderStatus
    {
        [Display(Name ="Sipariş Hazırlanıyor")]
        Hazırlanıyor=1,

        [Display(Name = "Sipariş Paketlendi")]
        Paketlendi,

        [Display(Name = "Kargoya Verildi")]
        Kargoda,

        [Display(Name = "Teslim Edildi")]
        TeslimEdildi,

        [Display(Name = "Tedarik Edilemedi")]
        TedarikEdilemedi,

        [Display(Name = "İptal Edildi")]
        İptalEdildi
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Enums
{
    //Stok Hareket Durumu
    public enum MovementType
    {

        //Stok giriş hareket durumu
        [Display(Name = "Devir Giriş")]
        CycleOut,

        [Display(Name = "Giriş Fişi")]
        InputeReceipt,

        [Display(Name = "Sayım Giriş")]
        CountIn,


        //Stok çıkış hareket durumu
        [Display(Name = "Fire Çıkış")]
        FireExit,

        [Display(Name = "Sarf Çıkış")]
        ConsumableOutput,

        [Display(Name = "Sayım Çıkış")]
        CountOut,


        //Satış olduğunda gerçekleşen stok hareket durumu
        [Display(Name = "Toptan Satış Faturası")]
        ToptanSatışFaturasıKDVHariç,

        [Display(Name = "Perakende Satış Faturası")]
        PerakendeSatışFaturasıKDVDahil,


        //Alış olduğunda gerçekleşen stok hareket durumu
        [Display(Name = "Alış Satış Faturası")]
        SatınalmaFaturası
    }
}

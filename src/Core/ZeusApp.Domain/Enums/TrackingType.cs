using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Enums
{
    public enum TrackingType
    {
        [Display(Name = "Takipsiz")]
        unfollow,

        [Display(Name ="Lot Numarası")]
        lotNumber,

        [Display(Name = "Seri Numarası")]
        seriNumber
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Enums;
public enum InvoiceType
{
    [Display(Name ="Alış")]
    buying,

    [Display(Name = "Satış")]
    selling
}

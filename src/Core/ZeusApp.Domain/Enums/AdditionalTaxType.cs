using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Domain.Enums;

public enum AdditionalTaxType
{
    [Display(Name = "Oran")]
    Rate,

    [Display(Name = "Fiyat")]
    Amount
}





using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Domain.Enums;

public enum DiscountType
{
    [Display(Name = "Tutar")]
    Amount,

    [Display(Name = "Oran")]
    Ratio
}


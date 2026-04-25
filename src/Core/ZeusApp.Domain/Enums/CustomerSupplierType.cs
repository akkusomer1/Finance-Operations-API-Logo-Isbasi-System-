using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Domain.Enums;

public enum CustomerSupplierType
{
    [Display(Name = "Müşteri")]
    Customer,

    [Display(Name = "Tedarikçi")]
    Supplier,

    [Display(Name = "Müşteri/Tedarikçi")]
    CustomerSupplier
}

public enum GeneralType
{
    [Display(Name = "Kurumsal")]
    Corporate,

    [Display(Name = "Bireysel")]
    Individual
}


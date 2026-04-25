using System.ComponentModel.DataAnnotations;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetAllPaged;
public class GetAllCustomerSuppliersResponse
{
    public int Id { get; set; }

    [Display(Name = "Müşteri Tipi")]
    public string CustomerGeneralType { get; set; }

    [Display(Name = "Müşteri / Tedarikçi Kodu:")]
    public string? CustomerSupplierCode { get; set; }

  
    [Display(Name = "Ad ve soyada yada Ünvan")]
    public string? NameOrTitle { get; set; }

    [Display(Name = "İl")]
    public string? City { get; set; }

    [Display(Name = "Telefon-1")]
    public string? PhoneNumber1 { get; set; }


    [Display(Name = "Açılış Bakiyesi")]
    public decimal OpeningBalance { get; set; }


    [Display(Name = "Aktif mi?")]
    public string Status { get; set; }
}
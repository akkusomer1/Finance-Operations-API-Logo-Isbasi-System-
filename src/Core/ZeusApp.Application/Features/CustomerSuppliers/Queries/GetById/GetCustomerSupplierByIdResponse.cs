using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.DTOs.Contact;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.DTOs.RelatedPerson;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetById;
public class GetCustomerSupplierByIdResponse
{

    public int Id { get; set; }

    public int Status { get; set; } = 1;


    /// <summary>
    /// Ünvan
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Müşteri / Tedarikçi Kodu:
    /// </summary>
    public string CustomerSupplierCode { get; set; }

    /// <summary>
    /// Kısa Ünvan
    /// </summary>
    public string ShortTitle { get; set; }

    /// <summary>
    /// Tipi *
    /// </summary>
    public CustomerSupplierType CustomerSupplierType { get; set; }
    public GeneralType GeneralType { get; set; }

    /// <summary>
    /// Vergi Dairesi
    /// </summary>
    public string TaxOffice { get; set; }

    /// <summary>
    /// Açılış Bakiyesi
    /// </summary>
    public decimal OpeningBalance { get; set; }

    /// <summary>
    /// Açılış Bakiyesi Tarihi
    /// </summary>
    public DateTime OpeningBalanceDate { get; set; }

    /// <summary>
    /// Döviz *
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// KDV Uygulanmaz
    /// </summary>
    public bool IsVATExempt { get; set; }

    /// <summary>
    /// Tevkifat Uygulanmaz
    /// </summary>
    public bool IsWithholdingTaxExempt { get; set; }

    /// <summary>
    /// Ek Vergi Uygulanmaz
    /// </summary>
    public bool IsAdditionalTaxExempt { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Kategori
    /// </summary>
    public int? CustomerCategoryId { get; set; }
    public CustomerCategory CustomerCategory { get; set; }

    /// <summary>
    /// Toplam Bakiye
    /// </summary>
    public decimal TotalBalance { get; set; }
    public ContactResponse Contact { get; set; }
    public List<RelatedPersonResponse> RelatedPersons { get; set; }=new List<RelatedPersonResponse>();
    public List<OtherAddressResponse> OtherAddresses { get; set; } = new List<OtherAddressResponse>();
    public List<BankResponse> Banks { get; set; } = new List<BankResponse>();
}

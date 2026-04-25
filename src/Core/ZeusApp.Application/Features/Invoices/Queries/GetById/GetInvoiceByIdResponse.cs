using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Invoice;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
public class GetInvoiceByIdResponse
{
    public int Id { get; set; }
    /// <summary>
    /// Fatura Tarihi
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Sevk Tarihi
    /// </summary>
    public DateTime ShipmentDate { get; set; }

    /// <summary>
    /// Vade Tarihi
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    ///Fatura Numarası
    /// </summary>
    public string? InvoiceNumber { get; set; }


    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType CurrencyType { get; set; }

    /// <summary>
    /// Döviz Kuru
    /// </summary>
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ara Toplam
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// İndirim Tipi
    /// </summary>
    public DiscountType DiscountType { get; set; }


    /// <summary>
    /// İndirim tutarı
    /// </summary>
    public decimal DiscountAmount { get; set; }


    /// <summary>
    /// Toplam İndirim
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Genel Toplam
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Toplam KDV Tutarı
    /// </summary>
    public decimal TotalVATAmount { get; set; }


    /// <summary>
    /// İndirimlerin düşürülmüş tutarı
    /// </summary>
    public decimal Total { get; set; }


    /// <summary>
    ///Satış Tipi-Toptan mı , Perakande mi?
    ///Toptansa Kvd hariç, Perakende ise kdv hariç
    ///Sadece Satış için gönderilecek.
    /// </summary>
    public SalesInvoiceType SalesInvoiceType { get; set; }


    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }


    /// <summary>
    /// Teslimat adresi farklı mı?
    /// </summary>
    public bool IsAddressDifferent { get; set; }


    /// <summary>
    /// Teslimat adresi (Eğer farklıysa)
    /// </summary>
    public int? OtherAddressId { get; set; }


    /// <summary>
    ///Kalan Tutar Tabloda göstereceğiz
    /// </summary>
    public decimal RemainingAmount { get; set; }


    /// <summary>
    /// Sevkiyat No 
    /// Sadece satış faturasında girilecektir.
    /// </summary>
    public string? ShipmentNumber { get; set; }


    /// <summary>
    /// Satış Fatura kategorisi
    /// </summary>
    /// 
    public int? InvoiceCategoryId { get; set; }

    /// <summary>
    /// Müşteriye ait bilgiler
    /// </summary>
    /// 
    [Required(ErrorMessage = "Müşteri alanı zorunludur.")]
    public int CustomerSupplierId { get; set; }


    /// <summary>
    /// T.C. Kimlik No
    /// </summary>
    public string? TcIdNumber { get; set; }

    // Kurumsal tedarikçi,müşteriye ait propertyler.
    /// <summary>
    /// Vergi No
    /// </summary>
    /// 
    public string? TaxNumber { get; set; }


    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// İl
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// İlçe
    /// </summary>
    public string District { get; set; }


    ///
    /// <summary>
    /// Ambar
    /// </summary>
    public int? HoldId { get; set; }

    /// <summary>
    /// Ürünler ve satış faturası arasında çoka çok ilişki ve tutulması gereken değerle burada tutuluyor.
    /// </summary>
    public ICollection<GetProductInvoiceResponse> ProductInvoices { get; set; } = new HashSet<GetProductInvoiceResponse>();

    public CustomerOtherAddressResponse CustomerOtherAddressResponse { get; set; }
}



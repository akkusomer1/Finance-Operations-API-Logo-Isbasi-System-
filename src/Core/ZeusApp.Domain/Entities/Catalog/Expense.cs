using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class Expense : AuditableEntity
{
    /// <summary>
    ///MüşteriTedarikçi
    /// </summary>
    public int? CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }


    ///// <summary>
    /////Ödeme Kasa ya da Bankası
    ///// </summary>
    //public int? CaseOrBankId { get; set; }
    //public CaseOrBank CaseOrBank { get; set; }

    /// <summary>
    /// Fiş / Fatura Tarihi
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Fiş / Fatura No
    /// </summary>
    public string InvoiceNo { get; set; }

    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// Ödeme Durumu
    /// </summary>
    public bool PaymentStatus { get; set; }

    ///// <summary>
    ///// Vade Tarihi
    ///// </summary>
    //public DateTime PaymentDate { get; set; }


    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///Gider Kategori
    /// </summary>
    public int? ExpenseCategoryId { get; set; }
    public ExpenseCategory ExpenseCategory { get; set; }

    /// <summary>
    /// Genel Toplam Tl veya Herhangi Döviz
    /// </summary>
    public decimal GrandTotal { get; set; }

    /// <summary>
    ///  Toplam indirimi
    /// </summary>
    public decimal DiscountTotal { get; set; }

    /// <summary>
    /// İndirim Türü
    /// </summary>
    public DiscountType? DiscountType { get; set; }

    public ICollection<ExpenseService> ExpenseServices { get; set; } = new HashSet<ExpenseService>();
}

using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Müşteri,tedarikçi ekleme alanındaki banka bilgisi
/// </summary>
public class Bank : AuditableEntity
{
    /// <summary>
    /// Banka Adı *
    /// </summary>
    public string BankName { get; set; }

    /// <summary>
    /// Şube Adı *
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// Şube Kodu
    /// </summary>
    public string BranchCode { get; set; }

    /// <summary>
    /// Hesap No *
    /// </summary>
    public string AccountNumber { get; set; }

    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// IBAN *
    /// </summary>
    public string IBAN { get; set; }

    public int CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }
}

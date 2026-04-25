using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class BankAccount : AuditableEntity
{
    /// <summary>
    /// Hesap Adı *
    /// </summary>
    public string AccountName { get; set; }
    /// <summary>
    /// Hesap No *
    /// </summary>
    public string AccountNumber { get; set; }

    /// <summary>
    /// IBAN *
    /// </summary>
    public string IBAN { get; set; }
    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// Açılış Bakiyesi
    /// </summary>
    public decimal OpeningBalance { get; set; }

    /// <summary>
    /// Açılış Bakiyesi Tarihi
    /// </summary>
    public DateTime OpeningBalanceDate { get; set; }

    /// <summary>
    /// Bakiyesi
    /// </summary>
    public decimal Balance { get; set; }
    /// <summary>
    /// Kredi Kartı
    /// </summary>
    public bool CreditCard { get; set; }

    public int GeneralBankId { get; set; }
    public GeneralBank  GeneralBank{ get; set; }

}

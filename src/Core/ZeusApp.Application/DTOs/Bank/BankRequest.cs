using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.DTOs.Bank;

public class BankRequest
{
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
}

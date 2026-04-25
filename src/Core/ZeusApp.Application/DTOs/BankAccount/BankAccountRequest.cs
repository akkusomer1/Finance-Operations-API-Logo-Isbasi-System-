using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.DTOs.BankAccount;
public class BankAccountRequest
{
    [Required(ErrorMessage = "Bu alan zorunludur")]
    public string AccountName { get; set; }

    [Required(ErrorMessage = "Bu alan zorunludur")]
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public CurrencyType? Currency { get; set; }
    public decimal OpeningBalance { get; set; }
    public DateTime OpeningBalanceDate { get; set; }
    public decimal Balance { get; set; }
    public bool CreditCard { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.BankAccounts.Queries.GetAllPaged;
public class GetAllBankAccountsResponse
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }

    /// <summary>
    /// Döviz Bakiye tek Prop gösterilcek
    /// </summary>
    public CurrencyType? Currency { get; set; }
    public decimal OpeningBalance { get; set; }

    public decimal Balance { get; set; }

    public string Status { get; set; }
}

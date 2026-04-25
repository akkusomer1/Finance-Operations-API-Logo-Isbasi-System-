using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.BankAccount;

namespace ZeusApp.Application.Features.GeneralBanks.Queries.GetById;
public class GetGeneralBankByIdResponse
{
    public int Id { get; set; }
    public string BankName { get; set; }

    public string BranchName { get; set; }

    public string BranchCode { get; set; }

    public string AuthorizedUser { get; set; }

    public string number { get; set; }

    public ICollection<BankAccountResponse> BankAccounts { get; set; } = new HashSet<BankAccountResponse>();
}

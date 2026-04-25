using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.DTOs.CaseOrBank;
public class CaseOrBankRequest
{
    public Case Cases { get; set; } = new Case();
   // public GeneralBank GeneralBank { get; set; } = new GeneralBank();
    public Domain.Entities.Catalog.BankAccount BankAccount { get; set; } = new Domain.Entities.Catalog.BankAccount();
}

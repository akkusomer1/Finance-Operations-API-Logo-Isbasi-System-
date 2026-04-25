using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Domain.Enums;
public enum LoanType
{
    [Display(Name = "Giriş")]
    loanOut,

    [Display(Name = "Çkış")]
    loanIn
}

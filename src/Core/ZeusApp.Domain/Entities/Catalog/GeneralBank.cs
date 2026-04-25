using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;
public class GeneralBank : AuditableEntity
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
    /// Yetkili Kişi
    /// </summary>
    public string AuthorizedUser { get; set; }
    /// <summary>
    /// Telefon
    /// </summary>
    public string number { get; set; }

    public ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();
}

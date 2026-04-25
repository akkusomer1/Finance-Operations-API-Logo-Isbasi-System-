using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Borç / Alacak
/// </summary>

public class Loan : AuditableEntity
{
    public LoanType LoanType { get; set; }
    public string DocumentNumber { get; set; }
    public int CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }
    public CurrencyType CurrencyType { get; set; }
    public DateTime LoanDate { get; set; }
    public decimal Amount { get; set; }
    public int? LoanCategoryId { get; set; }
    public LoanCategory? LoanCategory { get; set; }
    public string Description { get; set; }
}

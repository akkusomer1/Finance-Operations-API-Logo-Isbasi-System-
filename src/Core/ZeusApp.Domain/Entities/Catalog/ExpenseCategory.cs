using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;
public class ExpenseCategory : AuditableEntity
{
    public string Name { get; set; }

    public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
}

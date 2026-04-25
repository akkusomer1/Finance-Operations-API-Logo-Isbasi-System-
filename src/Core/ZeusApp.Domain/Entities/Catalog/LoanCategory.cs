using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;
public class LoanCategory:AuditableEntity
{
    public string Name { get; set; }

    public ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();

}

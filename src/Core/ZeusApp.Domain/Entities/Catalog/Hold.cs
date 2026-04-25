using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Ambar 
/// </summary>
public class Hold:AuditableEntity
{
    public string Name { get; set; }
    public ICollection<Invoice> Invoices { get; set; }=new HashSet<Invoice>();  
}

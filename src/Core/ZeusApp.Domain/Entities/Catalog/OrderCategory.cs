using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;
public class OrderCategory : AuditableEntity
{
    /// <summary>
    ///Yeni Alış/Satış Fatura Kategori
    /// </summary>
    public string Name { get; set; }
}

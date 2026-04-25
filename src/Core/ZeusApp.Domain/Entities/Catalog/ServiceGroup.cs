using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Entities.Catalog;
public class ServiceGroup:AuditableEntity
{
    public string Name { get; set; }
}

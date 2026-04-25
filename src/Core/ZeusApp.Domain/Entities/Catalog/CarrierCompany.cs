using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Abstractions.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class CarrierCompany : AuditableEntity
{
    public string Name { get; set; }
}
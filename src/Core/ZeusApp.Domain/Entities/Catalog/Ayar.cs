using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

public class Ayar : AuditableEntity
{
    public string Adi { get; set; }
    public string SistemAdi { get; set; }
    public string Degeri { get; set; }
    public byte[] Image { get; set; }
}

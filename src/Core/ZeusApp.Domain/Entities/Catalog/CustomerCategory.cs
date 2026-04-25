using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

public class CustomerCategory : AuditableEntity
{
    /// <summary>
    /// Müşteri Kategori Adı
    /// </summary>
    public string Name { get; set; } = null!;
    public ICollection<CustomerSupplier> CustomerSuppliers { get; set; }=new HashSet<CustomerSupplier>();   
}

//public class CustomerCategoryConfiguration : IEntityTypeConfiguration<CustomerCategory>
//{
//    public void Configure(EntityTypeBuilder<CustomerCategory> builder)
//    {
//        builder.HasMany(c => c.Customer_Suppliers)
//             .WithOne(x => x.CustomerCategory)
//             .HasForeignKey(x => x.CustomerCategoryId)
//             .OnDelete(DeleteBehavior.Restrict);
//    }
//}

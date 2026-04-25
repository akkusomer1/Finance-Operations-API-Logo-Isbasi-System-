using System;
using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;


/// <summary>
/// Hem kurumsal hemde bireysel müşteri,tedarikçiye ait property'ler.
/// Alış faturasında tedarikçiler istenecektir.
/// Satış faturalarında ise müşteriler istenecektir.
/// </summary>
public  class CustomerSupplier : AuditableEntity
{
    /// <summary>
    /// Ünvan
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Müşteri / Tedarikçi Kodu:
    /// </summary>
    public string CustomerSupplierCode { get; set; }

    /// <summary>
    /// Kısa Ünvan
    /// </summary>
    public string ShortTitle { get; set; }

    /// <summary>
    /// Tipi *
    /// </summary>
    public CustomerSupplierType CustomerSupplierType { get; set; }
    public GeneralType GeneralType { get; set; }

    /// <summary>
    /// Vergi Dairesi
    /// </summary>
    public string TaxOffice { get; set; }

    /// <summary>
    /// Açılış Bakiyesi
    /// </summary>
    public decimal OpeningBalance { get; set; }

    /// <summary>
    /// Açılış Bakiyesi Tarihi
    /// </summary>
    public DateTime OpeningBalanceDate { get; set; }

    /// <summary>
    /// Döviz *
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// KDV Uygulanmaz
    /// </summary>
    public bool IsVATExempt { get; set; }

    /// <summary>
    /// Tevkifat Uygulanmaz
    /// </summary>
    public bool IsWithholdingTaxExempt { get; set; }

    /// <summary>
    /// Ek Vergi Uygulanmaz
    /// </summary>
    public bool IsAdditionalTaxExempt { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Kategori
    /// </summary>
    public int? CustomerCategoryId { get; set; }
    public CustomerCategory CustomerCategory { get; set; }

    /// <summary>
    /// Toplam Bakiye
    /// </summary>
    public decimal TotalBalance { get; set; }




    // Sadece bireysel müşteriye ait alanlar
    /// <summary>
    /// T.C. Kimlik No
    /// </summary>
    public string? TcIdNumber { get; set; }

    /// <summary>
    /// Ad *
    /// </summary>
    public string? FirstName { get; set; } = null!;

    /// <summary>
    /// Soyad *
    /// </summary>
    public string? LastName { get; set; } = null!;


    // Kurumsal tedarikçi,müşteriye ait propertyler.
    /// <summary>
    /// Vergi No
    /// </summary>
    /// 
    public string? TaxNumber { get; set; }
    public Contact Contact { get; set; }
    public ICollection<Loan> Loans { get; set; } = new HashSet<Loan>();
    public ICollection<RelatedPerson> RelatedPersons { get; set; } = new HashSet<RelatedPerson>();
    public ICollection<OtherAddress> OtherAddresses { get; set; } = new HashSet<OtherAddress>();
    public ICollection<Bank> Banks { get; set; } = new HashSet<Bank>();
    public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
}


//public class Customer_SupplierConfiguration : IEntityTypeConfiguration<Customer_Supplier>
//{
//    public void Configure(EntityTypeBuilder<Customer_Supplier> builder)
//    {
//        builder.HasMany(x => x.RelatedPersons)
//            .WithOne(x => x.Customer_Supplier)
//            .HasForeignKey(x => x.Customer_SupplierId)
//            .OnDelete(DeleteBehavior.Cascade);

//        builder.HasMany(x => x.OtherAddresses)
//            .WithOne(x => x.Customer_Supplier)
//            .HasForeignKey(x => x.Customer_SupplierId)
//            .OnDelete(DeleteBehavior.Cascade);

//        builder.HasMany(x => x.Banks)
//           .WithOne(x => x.Customer_Supplier)
//           .HasForeignKey(x => x.Customer_SupplierId)
//           .OnDelete(DeleteBehavior.Cascade);

//        builder.HasOne(x => x.Contact)
//            .WithOne(x => x.Customer_Supplier)
//            .HasForeignKey<Contact>(x => x.Customer_SupplierId)
//            .OnDelete(DeleteBehavior.Cascade);
//    }
//}

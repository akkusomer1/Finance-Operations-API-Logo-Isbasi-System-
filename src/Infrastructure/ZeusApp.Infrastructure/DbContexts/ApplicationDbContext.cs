using System.Data;
using System.Linq.Expressions;
using System.Reflection.Emit;
using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Contexts;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Contexts;
using ZeusApp.Application.Interfaces.Shared;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.DbContexts;

public class ApplicationDbContext : AuditableContext, IApplicationDbContext
{
    private readonly IDateTimeService _dateTime;
    private readonly IAuthenticatedUserService _authenticatedUser;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
    {
        _dateTime = dateTime;
        _authenticatedUser = authenticatedUser;
    }

    public DbSet<Ayar> Ayarlar { get; set; }

    public DbSet<ProductService> ProductServices { get; set; }
    public DbSet<ProductServiceCategory> ProductServiceCategories { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<CustomerSupplier> CustomerSuppliers { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<OtherAddress> OtherAddresses { get; set; }
    public DbSet<RelatedPerson> RelatedPersons { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CustomerCategory> CustomerCategories { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<LoanCategory> LoanCategories { get; set; }
    public DbSet<OrderCategory> OrderCategories { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockCategory> StockCategories { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    public DbSet<ProductInvoice> ProductInvoices { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceCategory> InvoiceCategories { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<ExpenseService> ExpenseServices { get; set; }
    public DbSet<Hold> Holds { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<GeneralBank> GeneralBanks { get; set; }
    public DbSet<CarrierCompany> CarrierCompanies { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }


    #region Connection
    public IDbConnection Connection => Database.GetDbConnection();
    public bool HasChanges => ChangeTracker.HasChanges();
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = _dateTime.NowUtc;
                    entry.Entity.CreatedBy = _authenticatedUser.UserId;
                    entry.Entity.Status = EntityStatus.Active; // durum aktif olarak ekleniyor
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                    entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                    break;
            }
        }

        return _authenticatedUser.UserId == null
            ? await base.SaveChangesAsync(cancellationToken)
            : await base.SaveChangesAsync(_authenticatedUser.UserId);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,8)");
        }

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        #region Set HasQueryFilter For Deleted Entities
        // deleted olarak işaretlenmiş varlıkların sorgulardan dışlanmasını (filtrelenmesini) sağlar
        foreach (var entityType in builder.Model.GetEntityTypes().Where(e => e.GetProperties().Any(p => typeof(EntityStatus).Equals(p.ClrType))))
        {
            var parameter = Expression.Parameter(entityType.ClrType, "x");
            var propertyName = entityType.GetProperties().First(p => typeof(EntityStatus).Equals(p.ClrType)).Name;
            var leftExpression = Expression.Call(typeof(EF), "Property", new[] { typeof(EntityStatus) }, parameter, Expression.Constant(propertyName));
            builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(Expression.NotEqual(leftExpression, Expression.Constant(EntityStatus.Deleted)), parameter));
        }
        #endregion

        builder.Entity<CustomerSupplier>().HasMany(x => x.RelatedPersons)
            .WithOne(x => x.CustomerSupplier)
            .HasForeignKey(x => x.CustomerSupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CustomerSupplier>().HasMany(x => x.OtherAddresses)
            .WithOne(x => x.CustomerSupplier)
            .HasForeignKey(x => x.CustomerSupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CustomerSupplier>().HasMany(x => x.Banks)
           .WithOne(x => x.CustomerSupplier)
           .HasForeignKey(x => x.CustomerSupplierId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CustomerSupplier>().HasOne(x => x.Contact)
            .WithOne(x => x.CustomerSupplier)
            .HasForeignKey<Contact>(x => x.CustomerSupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<GeneralBank>().HasMany(x => x.BankAccounts)
           .WithOne(x => x.GeneralBank)
           .HasForeignKey(x => x.GeneralBankId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Invoice>().HasMany(x => x.ProductInvoices)
          .WithOne(x => x.Invoice)
          .HasForeignKey(x => x.InvoiceId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProductService>().HasMany(x => x.ProductInvoices)
     .WithOne(x => x.ProductService)
     .HasForeignKey(x => x.ProductServiceId)
     .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(builder);
    }
    #endregion
}

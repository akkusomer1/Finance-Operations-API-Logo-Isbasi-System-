using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class ExpenseService : AuditableEntity
{
    //expense servici ile 1 e ço kilişki olmalı 1 expensede fazlaca  hizmet olabilir çoka çok değil  
    // hizmetleri çekip ilgili propertileri alıcaz bize lazım olan propertiler
    public int ExpenseId { get; set; }
    public int ProductServiceId { get; set; }

    public Expense Expense { get; set; }
    public ProductService ProductService { get; set; }
    /// <summary> 
    /// Miktar
    /// Eklenmedi Eklenmesi gerekiyor
    /// </summary>
    public int quantity { get; set; }


    /// <summary>
    ///   Birim
    /// </summary>
    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }

    /// <summary>
    /// KDV Oranı (%)
    /// </summary>
    public int VATRate { get; set; }

    /// <summary>
    /// İndirim miktari
    /// </summary>
    public int? Discount { get; set; }

    /// <summary>
    /// İndirim Türü
    /// </summary>
    public DiscountType? DiscountType { get; set; }

    /// <summary>
    /// Genel Toplam Tl veya Herhangi Döviz
    /// </summary>
    public decimal GrandTotal { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.DTOs.Expense;
public class ExpenseServiceCreateRequest
{
    public int ProductServiceId { get; set; }
    public int UnitId { get; set; }

    // public int quantity { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }
    public int VATRate { get; set; }
    public int? Discount { get; set; }
    public DiscountType? DiscountType { get; set; }
    public decimal GrandTotal { get; set; }
}

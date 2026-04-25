using System.ComponentModel.DataAnnotations;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Loans.Queries.GetById;
public class GetLoanByIdResponse
{
    public int Id { get; set; }
    [Display(Name = "Giriş/Çıkış")]
    public LoanType LoanType { get; set; }
    [Display(Name = "Makbuz/Belge No")]
    public string DocumentNumber { get; set; }
    [Display(Name = "Müşteri/Tedarikçi Adı")]
    public int CustomerSupplierId { get; set; }
    [Display(Name = "Tarih")]
    public DateTime LoanDate { get; set; }
    [Display(Name = "Tutar")]
    public decimal Amount { get; set; }
    [Display(Name = "Kategori")]
    public int LoanCategoryId { get; set; }
    [Display(Name = "Açıklama")]
    public string Description { get; set; }
}

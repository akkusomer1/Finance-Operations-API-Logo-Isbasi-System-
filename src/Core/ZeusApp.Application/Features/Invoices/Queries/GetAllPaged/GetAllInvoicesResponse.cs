using System.Linq.Expressions;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Invoices.Queries.GetAllPaged;
public class GetAllInvoicesResponse : IRequest<PaginatedResult<GetAllInvoicesResponse>>
{
    public int Id { get; set; }
    public string Status { get; set; }

    /// <summary>
    ///Satış Tipi-Toptan mı , Perakande mi?
    ///Toptansa Kvd hariç, Perakende ise kdv hariç
    ///Sadece Satış için gönderilecek.
    /// </summary>
    public SalesInvoiceType SalesInvoiceType { get; set; }

    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }

    /// <summary>
    /// Fatura Tarihi
    /// </summary>
    public DateTime InvoiceDate { get; set; }
    public string NameOrTitle { get; set; }

    /// <summary>
    ///Fatura Numarası
    /// </summary>
    public string? InvoiceNumber { get; set; }

    /// <summary>
    /// Vade Tarihi
    /// </summary>
    public DateTime DueDate { get; set; }


    /// <summary>
    /// Genel Toplam
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    ///Kalan Tutar Tabloda göstereceğiz
    /// </summary>
    public decimal RemainingAmount { get; set; }

    /// <summary>
    /// Döviz kuru eğer Tl değilse döviz olarak hesapla
    /// </summary>
    
    public string? ExchanceCalculate { get; set; }

}

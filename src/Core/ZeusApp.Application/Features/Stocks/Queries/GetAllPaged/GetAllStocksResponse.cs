using System.Linq.Expressions;
using AspNetCoreHero.Abstractions.Enums;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Extensions;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Queries.GetAllPaged;
public class GetAllStocksResponse 
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    /// <summary>
    /// Genel Stok  Harektet Türü
    /// </summary>
    public MovementType MovementType { get; set; }


    public string Status { get; set; }
    public string CustomerOrSupplierName { get; set; }


    /// <summary>
    /// Genel Toplam Tl veya Herhangi Döviz
    /// </summary>
    public decimal GrandTotal { get; set; }

    /// <summary>
    /// Son düzenlenme zamanı
    /// </summary>
    public DateTime? LastModifiedOn { get; set; }
}



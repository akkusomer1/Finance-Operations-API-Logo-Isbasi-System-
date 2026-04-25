using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Queries.GetAllPaged;
public class GetAllOrdersResponse
{
    public int Id { get; set; }


    /// <summary>
    /// Sipariş Tarihi
    /// </summary>
    public DateTime OrderDate { get; set; }


    public string NameOrTitle { get; set; }

    /// <summary>
    ///Sipariş Numarası
    /// </summary>
    public string? OrderNumber { get; set; }



    /// <summary>
    /// Genel Toplam
    /// </summary>
    public decimal TotalAmount { get; set; }


    /// <summary>
    /// Ödemeli mi?
    /// </summary>
    public bool HasPaid { get; set; }

    /// <summary>
    /// Sipariş kategori adı
    /// </summary>
    public string OrderCategoryName { get; set; }

    /// <summary>
    /// Sipariş Durumu
    /// </summary>
    public OrderStatus OrderStatus { get; set; }


    /// <summary>
    /// Döviz kuru eğer Tl değilse döviz olarak hesapla
    /// </summary>

    public string? ExchanceCalculate { get; set; }
}

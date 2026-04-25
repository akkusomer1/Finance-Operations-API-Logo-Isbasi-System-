using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Invoice;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Queries.GetById
{
    public class GetOrderInvoiceByIdResponse
    {
        public int OrderId { get; set; }


        /// <summary>
        /// Müşteriye ait bilgiler
        /// </summary>
        /// 
        [Required(ErrorMessage = "Müşteri alanı zorunludur.")]
        public int CustomerSupplierId { get; set; }

        /// <summary>
        /// Döviz
        /// </summary>
        public CurrencyType CurrencyType { get; set; }


        /// <summary>
        /// İndirim tutarı
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// İndirim Tipi
        /// </summary>
        public DiscountType DiscountType { get; set; }


        /// <summary>
        /// Döviz Kuru
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Toplam KDV Tutarı
        /// </summary>
        public decimal TotalVATAmount { get; set; }


        /// <summary>
        /// Toplam İndirim
        /// </summary>
        public decimal TotalDiscount { get; set; }


        /// <summary>
        /// Genel Toplam
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// İndirimlerin düşürülmüş tutarı
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Ara Toplam
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        ///Satış faturası mı, alış faturası mı?
        /// </summary>
        public InvoiceType InvoiceType { get; set; }
        public OrderInvoiceType OrderInvoiceType { get; set; }

        public ICollection<GetProductOrderResponse> ProductOrders { get; set; } = new HashSet<GetProductOrderResponse>();
    }
}

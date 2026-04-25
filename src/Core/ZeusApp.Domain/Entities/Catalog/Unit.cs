using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeusApp.Domain.Entities.Catalog
{
    public class Unit:AuditableEntity
    {
        /// <summary>
        /// Birim Adı
        /// </summary>
        public string Name { get; set; }

        public ICollection<ProductService> Products { get; set; }=new HashSet<ProductService>();
        public ICollection<ProductStock> ProductStocks { get; set; }=new HashSet<ProductStock>();
        public ICollection<ExpenseService> ExpenseServices { get; set; }=new HashSet<ExpenseService>();
        public ICollection<ProductInvoice> ProductInvoices { get; set; }=new HashSet<ProductInvoice>();
        public ICollection<ProductOrder> ProductOrders { get; set; }=new HashSet<ProductOrder>();
    }
}

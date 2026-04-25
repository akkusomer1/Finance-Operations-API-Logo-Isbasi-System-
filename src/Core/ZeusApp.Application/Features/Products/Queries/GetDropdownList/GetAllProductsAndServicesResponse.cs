using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.Enums;

namespace ZeusApp.Application.Features.Products.Queries.GetDropdownList;
public class GetAllProductsAndServicesResponse
{
    public int Id { get; set; }
    public string ProductOrServiceName { get; set; }
    public decimal TotalStockAmount { get; set; }
    public decimal UnitPrice { get; set; }
    public string UnitName { get; set; }
}


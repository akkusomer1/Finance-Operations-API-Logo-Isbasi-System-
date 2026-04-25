using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
public class GetAllCustomerOrSupplierResponse
{
    public int Id { get; set; }

    /// <summary>
    /// Toplam Bakiye
    /// </summary>
    public decimal TotalBalance { get; set; }


    /// <summary>
    /// İsim veya ünvan
    /// </summary>
    public string FullNameOrTitle { get; set; }

}

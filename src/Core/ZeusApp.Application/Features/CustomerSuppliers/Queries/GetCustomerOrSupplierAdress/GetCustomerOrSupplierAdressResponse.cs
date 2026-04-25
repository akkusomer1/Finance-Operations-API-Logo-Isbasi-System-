using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.CustomerSuppliers.Queries.GetCustomerOrSupplierAdress;
public class GetCustomerOrSupplierAdressResponse
{
    public int Id { get; set; }
    // Sadece bireysel müşteriye ait alanlar
    /// <summary>
    /// T.C. Kimlik No
    /// </summary>
    public string? TcIdNumber { get; set; }

    // Kurumsal tedarikçi,müşteriye ait propertyler.
    /// <summary>
    /// Vergi No
    /// </summary>
    /// 
    public string? TaxNumber { get; set; }


    /// <summary>
    /// Adres
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// İl
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// İlçe
    /// </summary>
    public string District { get; set; }

    public List<CustomerOtherAddressResponse> CustomerOtherAddressResponse { get; set; }=new List<CustomerOtherAddressResponse>();   
}

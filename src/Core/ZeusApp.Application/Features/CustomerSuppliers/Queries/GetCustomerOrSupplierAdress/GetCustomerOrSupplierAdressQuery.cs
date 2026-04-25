using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.CustomerSuppliers.Queries.GetCustomerOrSupplierAdress;
public class GetCustomerOrSupplierAdressQuery : IRequest<Result<GetCustomerOrSupplierAdressResponse>>
{
    public int Id { get; set; }
}



public class GetCustomerOrSupplierAdressQueryHandle : IRequestHandler<GetCustomerOrSupplierAdressQuery, Result<GetCustomerOrSupplierAdressResponse>>
{
    private readonly ICustomerSupplierRepository _repository;

    public GetCustomerOrSupplierAdressQueryHandle(ICustomerSupplierRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCustomerOrSupplierAdressResponse>> Handle(GetCustomerOrSupplierAdressQuery request, CancellationToken cancellationToken)
    {

        var customerAdressInfo = await _repository.CustomerSuppliers
               .Include(x => x.Contact)
               .Include(x => x.OtherAddresses).Select(x => new GetCustomerOrSupplierAdressResponse
               {
                   Id = x.Id,
                   TaxNumber = x.TaxNumber,
                   TcIdNumber = x.TcIdNumber,
                   Address = x.Contact.Address,
                   City = x.Contact.City,
                   Country = x.Contact.Country,
                   District = x.Contact.District,
                   CustomerOtherAddressResponse = x.OtherAddresses.Select(x => new CustomerOtherAddressResponse
                   {
                       Id = x.Id,
                       Address = x.Address,
                       City = x.City,
                       AddressTitle = x.AddressTitle,
                       Country = x.Country,
                       District = x.District

                   }).ToList()
               }).SingleOrDefaultAsync(x => x.Id == request.Id);


        if (customerAdressInfo==null)
        {
            throw new KeyNotFoundException("Bu isimde bir Müşteri/Tedarikçi bulunamadı!");
        }
        return Result<GetCustomerOrSupplierAdressResponse>.Success(customerAdressInfo, 200);
    }


}
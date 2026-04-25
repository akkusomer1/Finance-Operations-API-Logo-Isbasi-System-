using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
public class GetInvoiceByIdQuery : IRequest<Result<GetInvoiceByIdResponse>>
{
    public int Id { get; set; }

}

public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, Result<GetInvoiceByIdResponse>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IOtherAddressRepository _otherAddressRepository;
    private readonly IMapper _mapper;

    public GetInvoiceByIdQueryHandler(IInvoiceRepository invoiceRepository, IMapper mapper, IOtherAddressRepository otherAddressRepository)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _otherAddressRepository = otherAddressRepository;
    }

    public async Task<Result<GetInvoiceByIdResponse>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.Id);

        var invoiceMapped = _mapper.Map<GetInvoiceByIdResponse>(invoice);


        if (invoice.OtherAddressId != null && invoice.IsAddressDifferent)
        {
            var otherAdress = await _otherAddressRepository.GetByIdAsync(invoice.OtherAddressId.Value);
            CustomerOtherAddressResponse otherAdressResp = new()
            {
                Id = request.Id,
                Address = otherAdress.Address,
                AddressTitle = otherAdress.AddressTitle,
                City = otherAdress.City,
                Country = otherAdress.Country,
                District = otherAdress.District
            };
            invoiceMapped.CustomerOtherAddressResponse = otherAdressResp;
        }
        return Result<GetInvoiceByIdResponse>.Success(invoiceMapped, 200);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Orders.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Commands.Delete;
public class DeleteOrderCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result<int>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _productRepository;
    private readonly ICustomerSupplierRepository _customerSupplierRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository,
        IUnitOfWork unitOfWork, IMapper mapper, IProductServiceRepository productRepository, ICustomerSupplierRepository customerSupplierRepository)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _customerSupplierRepository = customerSupplierRepository;
    }


    //Eğer siparişi faturalandırmadan silerse.
    //Müşterini/Tedarikçi bakiyesini güncelleriz.

    //Eğerki siparişe bağlı olan fatura varsa silinmesini engelliyor önce faturayı sil deniyor.
    //Eğer siparişi faturalandıysa yani status closed veya sending ise silemezsin.

    //Eğer bu siparişe bağlı faturayı silerse tekrardan bu alanları güncellemek gerekir.


    public async Task<Result<int>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        if (order == null)
        {
            throw new KeyNotFoundException("Böyle bir sipariş bulunamadı.");
        }

        if (order.OrderStatus != OrderStatus.waiting)
        {
            throw new Exception("Bu sipariş faturalandırılmıştır, silinemez!");
        }

        if (order.HasPaid)
        {
            //Eğerki gelen değer satış ve perakende satışssa = Bitti
            if (request.InvoiceType == InvoiceType.selling)
            {
                //Toplam tutarı Tl olarak düşürmek gerekir.
                if (order.CurrencyType != CurrencyType.TL)
                {
                    order.TotalAmount = order.ExchangeRate * order.TotalAmount;
                }

                order.CustomerSupplier.TotalBalance -= order.TotalAmount;
            }
          
            //Eğerki alış faturası seçildiyse
            if (request.InvoiceType == InvoiceType.buying)
            {
                //Toplam tutarı Tl olarak arttırmak gerekir.
                if (order.CurrencyType != CurrencyType.TL)
                {
                    order.TotalAmount = order.ExchangeRate * order.TotalAmount;
                }
                order.CustomerSupplier.TotalBalance += order.TotalAmount;
              
                await _customerSupplierRepository.UpdateAsync(order.CustomerSupplier);
            }
        }
        await _orderRepository.DeleteAsync(order);
        return Result<int>.Success(order.Id, 200);
    }
}
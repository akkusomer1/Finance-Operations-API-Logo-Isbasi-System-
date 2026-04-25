using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CustomerSupplieries.Commands.Delete;
public class DeleteCustomerSupplierCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public DeleteCustomerSupplierCommand(int id)
    {
        Id = id;
    }
    public class DeleteCustomerSupplierCommandHandler : IRequestHandler<DeleteCustomerSupplierCommand, Result<int>>
    {

        private readonly ICustomerSupplierRepository _customerSupplierRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IOtherAddressRepository _otherAddressRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IRelatedPersonRepository _relatedPersonRepository;



        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerSupplierCommandHandler(ICustomerSupplierRepository customerSupplierRepository, IUnitOfWork unitOfWork, IContactRepository contactRepository, IOtherAddressRepository otherAddressRepository, IBankRepository bankRepository, IRelatedPersonRepository relatedPersonRepository)
        {
            _customerSupplierRepository = customerSupplierRepository;
            _unitOfWork = unitOfWork;
            _contactRepository = contactRepository;
            _otherAddressRepository = otherAddressRepository;
            _bankRepository = bankRepository;
            _relatedPersonRepository = relatedPersonRepository;
        }

        public async Task<Result<int>> Handle(DeleteCustomerSupplierCommand command, CancellationToken cancellationToken)
        {
            var customerSupplier = await _customerSupplierRepository.GetByIdAsync(command.Id, true);

            if (customerSupplier == null)
                throw new KeyNotFoundException("Bu isimde bir Müşteri/Tedarikçi bulunamadı");

            await _customerSupplierRepository.DeleteAsync(customerSupplier);
            customerSupplier.RelatedPersons.ToList().ForEach(async x =>
            {
                await _relatedPersonRepository.DeleteAsync(x);
            });
            customerSupplier.Banks.ToList().ForEach(async x =>
            {
                await _bankRepository.DeleteAsync(x);
            });

            customerSupplier.OtherAddresses.ToList().ForEach(async x =>
            {
                await _otherAddressRepository.DeleteAsync(x);
            });
            await _contactRepository.DeleteAsync(customerSupplier.Contact);

            await _unitOfWork.Commit(cancellationToken);

            return Result<int>.Success(customerSupplier.Id, 200);
        }
    }
}

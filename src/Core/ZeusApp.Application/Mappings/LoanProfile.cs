using AutoMapper;
using ZeusApp.Application.Features.Loans.Commands.Create;
using ZeusApp.Application.Features.Loans.Commands.Update;
using ZeusApp.Application.Features.Loans.Queries.GetAllPaged;
using ZeusApp.Application.Features.Loans.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class LoanProfile:Profile
{
    public LoanProfile()
    {
        CreateMap<CreateLoanCommand, Loan>().ReverseMap();
        CreateMap<UpdateLoanCommand, Loan>().ReverseMap();
        CreateMap<GetLoanByIdResponse, Loan>().ReverseMap();
        CreateMap<GetAllLoansResponse, Loan>().ReverseMap();
    }
}

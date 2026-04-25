using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.Features.CarrierCompanies.Commands.Create;
using ZeusApp.Application.Features.CarrierCompanies.Queries.GetAllPaged;
using ZeusApp.Application.Features.CarrierCompanies.Queries.GetById;
using ZeusApp.Application.Features.Cases.Commands.Create;
using ZeusApp.Application.Features.Cases.Queries.GetAllPaged;
using ZeusApp.Application.Features.Cases.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings
{
    public class CarrierCompanyProfile:Profile
    {
        public CarrierCompanyProfile()
        {
            CreateMap<CreateCarrierCompanyCommand, CarrierCompany>().ReverseMap();
            CreateMap<GetCarrierCompanyByIdResponse, CarrierCompany>().ReverseMap();
            CreateMap<GetAllCarrierCompaniesResponse, CarrierCompany>().ReverseMap();
        }
    }
}

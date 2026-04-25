using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.DTOs.RelatedPerson;
using ZeusApp.Application.Features.RelatedPersons.Commands.Create;
using ZeusApp.Application.Features.RelatedPersons.Queries.GetAllPaged;
using ZeusApp.Application.Features.RelatedPersons.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class RelatedPersonProfile : Profile
{
    public RelatedPersonProfile()
    {
        CreateMap<CreateRelatedPersonCommand, RelatedPerson>().ReverseMap();
        CreateMap<GetRelatedPersonByIdResponse, RelatedPerson>().ReverseMap();
        CreateMap<GetAllRelatedPersonsResponse, RelatedPerson>().ReverseMap();



        CreateMap<RelatedPersonResponse, RelatedPerson>().ReverseMap();
        CreateMap<RelatedPersonRequest, RelatedPerson>().ReverseMap();
    }
}

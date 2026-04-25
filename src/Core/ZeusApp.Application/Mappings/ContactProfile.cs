using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.DTOs.Contact;
using ZeusApp.Application.Features.Contacts.Commands.Create;
using ZeusApp.Application.Features.Contacts.Queries.GetAllPaged;
using ZeusApp.Application.Features.Contacts.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<CreateContactCommand, Contact>().ReverseMap();
        CreateMap<GetContactByIdResponse, Contact>().ReverseMap();
        CreateMap<GetAllContactsResponse, Contact>().ReverseMap();

        CreateMap<ContactResponse, Contact>().ReverseMap();
        CreateMap<ContactRequest, Contact>().ReverseMap();
    }
}

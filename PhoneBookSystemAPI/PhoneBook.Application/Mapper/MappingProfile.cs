using AutoMapper;
using PhoneBook.DTOs.ContactDTOs;
using PhoneBook.Model;

namespace PhoneBook.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, CUContactDTO>().ReverseMap();

    }
}


using AutoMapper;
using Invite.Entities.Models;
using Invite.Entities.Responses;

namespace Invite.Services.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ResponsibleModel, ResponsibleResponse>();
        CreateMap<PersonModel, PersonResponse>();
    }
}


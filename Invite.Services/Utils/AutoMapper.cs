using AutoMapper;
using Invite.Entities.Models;
using Invite.Entities.Responses;

namespace Invite.Services.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HallModel, HallResponse>();
        CreateMap<BuffetModel, BuffetResponse>();
        CreateMap<EventModel, EventResponse>();
    }
}


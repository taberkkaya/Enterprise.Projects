using Application.Features.Models.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, GetListModelListItemDto>().ReverseMap();
        CreateMap<IPaginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
    }
}

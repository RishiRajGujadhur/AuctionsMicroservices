using AutoMapper;
using Contracts;
using SearchService.Entities;

namespace SearchServiceService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuctionCreated, Item>().IncludeMembers();
    }
}
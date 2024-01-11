using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
        CreateMap<Item, AuctionDto>().IncludeMembers();
        CreateMap<AuctionDto, Auction>().IncludeMembers();
        CreateMap<CreateAuctionDto, Auction>().IncludeMembers()
           .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<CreateAuctionDto,Item>();
        CreateMap<AuctionDto, AuctionCreated>();
    }
}
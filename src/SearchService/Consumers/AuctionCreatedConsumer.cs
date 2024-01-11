using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService;

public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{

    public AuctionCreatedConsumer(IMapper mapper)
    {
        Mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task Consume(ConsumeContext<AuctionCreated> context){
        Console.WriteLine("Consuming Created auction " +  context.Message.Id);
        var item = Mapper.Map<Item>(context.Message);
        await item.SaveAsync();
    }
}
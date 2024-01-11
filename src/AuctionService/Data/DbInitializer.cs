using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DbInitializer{
    public static void InitDb(WebApplication app){
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
    }
    
    public static void SeedData(AuctionDbContext context){
        context.Database.Migrate();

        if(context.Auctions.Any()){
          Console.WriteLine("Data is already there.");
          return;
        }

      var auctions = new List<Auction>(){
            new Auction
            {
                Id = Guid.Parse("88889e2b-4ed3-4f58-9a69-ad195951bd55"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "Jim",
                AuctionEnd = DateTime.UtcNow.AddDays(45),
                Item = new Item
                {
                    Make = "Ferrari",
                    Model = "Spider",
                    Color = "Red",
                    Year = "2015",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"
                }
            },
            new Auction
            {
                Id = Guid.Parse("8d22ff01-da34-4737-b23f-bfdef3805dba"),
                Status = Status.Live,
                ReservePrice = 150000,
                Seller = "Tod",
                AuctionEnd = DateTime.UtcNow.AddDays(13),
                Item = new Item
                {
                    Make = "Ferrari",
                    Model = "F-430",
                    Color = "Red",
                    Year = "2022",
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"
                }
            }
      };

      context.AddRange(auctions);
      context.SaveChanges();
    }
}
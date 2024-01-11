using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

[Table("Items")]
public class Item
{
   public Guid Id { get; set; }
   public string Make { get; set; }
   public string Model { get; set; }
   public string Year { get; set; }
   public string Color { get; set; }
   public int Millage { get; set; }
   public string ImageUrl { get; set; }

   // Nav properties 1 to 1 relationship between Item and Auction
   public Auction Auction { get; set; }
   public Guid AuctionId { get; set; }
}
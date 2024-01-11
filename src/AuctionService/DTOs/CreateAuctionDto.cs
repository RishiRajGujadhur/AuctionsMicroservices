using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;

public class CreateAuctionDto
{
    [Required]
    public string Make { get; set; }
     [Required]
    public string Model { get; set; }
     [Required]
    public string Year { get; set; }
     [Required]
    public string Color { get; set; }
     [Required]
    public int Millage { get; set; }
     [Required]
    public string ImageUrl { get; set; }
     [Required]
    public int ReservePrice { get; set; }
     [Required]
    public DateTime AuctionEnd { get; set; }

}
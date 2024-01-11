using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs;

public class UpdateAuctionDto
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string Year { get; set; }
    public string Color { get; set; }
    public int? Millage { get; set; }
    public string ImageUrl { get; set; }
}
using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
    private readonly AuctionDbContext context;
    private readonly IMapper mapper;

    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await this.context.Auctions.Include(x => x.Item)
        .OrderBy(x => x.Item.Make).ToListAsync();
        return this.mapper.Map<List<AuctionDto>>(auctions);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await this.context.Auctions.Include(x => x.Item)
        .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();

        return this.mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = this.mapper.Map<Auction>(auctionDto);
        auction.Seller = "test";
        context.Auctions.Add(auction);
        var result = await context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Saving failed!");
        return CreatedAtAction(nameof(GetAuctionById),
            new { auction.Id }, this.mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await this.context.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Millage = updateAuctionDto.Millage ?? auction.Item.Millage;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;

        var result = await this.context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest("Problem updating changes");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await this.context.Auctions.FindAsync(id);
        this.context.Auctions.Remove(auction);
          var result = await this.context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Problem deleting changes");
    }
}


using System;
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
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    public AuctionsController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.ProjectName)
            .ToListAsync();

        if (!auctions.Any())
        {
            return NoContent();
        }

        var detailedAuctions = _mapper.Map<List<AuctionDto>>(auctions);

        return Ok(detailedAuctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item) // load item data
            .FirstOrDefaultAsync(x => x.Id == id); // find first matching auction else null

        if (auction == null) return NotFound();

        var detailedAuction = _mapper.Map<AuctionDto>(auction);

        return Ok(detailedAuction);

    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
    {
        var auction = _mapper.Map<Auction>(auctionDto);

        // TODO: Add current user as seller (identity server)
        auction.Seller = "test";
        _context.Auctions.Add(auction);

        // Check if there is any changes in database
        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not save changes to DB.");

        // Returns a 201 Created response with the location and details of the newly created auction.
        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
    {
        var auction = await _context.Auctions.Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();

        // TODO: Check seller == username

        // TODO:  Consider if updates are necessary in production
        auction.Item.ProjectName = updateAuctionDto.ProjectName ?? auction.Item.ProjectName;
        auction.Item.Description = updateAuctionDto.Description ?? auction.Item.Description;
        auction.Item.Category = Enum.TryParse(updateAuctionDto.Category, out Category category)
            ? category
            : auction.Item.Category;
        auction.Item.ItemStatus = Enum.TryParse(updateAuctionDto.ItemStatus, out ItemStatus itemStatus)
            ? itemStatus
            : auction.Item.ItemStatus;
        auction.Item.ImageUrl = updateAuctionDto.ImageUrl ?? auction.Item.ImageUrl;

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest("Error Saving Changes");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);

        if (auction == null) return NotFound();


        // TODO: Check seller == username

        // TODO: Consider if deletes should be allowed in production
        _context.Auctions.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) return BadRequest("Could not update DB");

        return Ok();
    }


}

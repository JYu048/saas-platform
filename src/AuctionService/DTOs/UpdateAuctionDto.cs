using System;

namespace AuctionService.DTOs;


public class UpdateAuctionDto
{
    public string? ProjectName { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? ItemStatus { get; set; }
    public string? ImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public DateTime? AuctionEnd { get; set; }
}

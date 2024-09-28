using System;

namespace AuctionService.DTOs;


public class UpdateAuctionDto
{
    // ALL of the required fields below must be passed even if no changes are made
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string ItemStatus { get; set; }
    public required string ImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public DateTime? AuctionEnd { get; set; }
}

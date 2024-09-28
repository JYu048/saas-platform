using System;

namespace AuctionService.DTOs;

// TODO: Change nullability after assessing requirements from client and other services
public class AuctionDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? AuctionEnd { get; set; }
    public required string Seller { get; set; }
    public string? Winner { get; set; }
    public required string Status { get; set; }
    public int ReservePrice { get; set; } = 0;
    public int SoldAmount { get; set; }
    public int CurrentHighBid { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string ItemStatus { get; set; }
    public required string ImageUrl { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }
}



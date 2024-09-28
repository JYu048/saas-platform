using System;
using MongoDB.Entities;

namespace SearchService.Entities;

public class Item : Entity
{
    public int ReservePrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public required string Seller { get; set; }
    public string? Winner { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string ItemStatus { get; set; }
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public string? ImageUrl { get; set; }
    public int SoldAmount { get; set; }
    public int CurrentHighBid { get; set; }
}


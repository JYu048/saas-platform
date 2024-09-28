using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

[Table("Items")]

public class Item
{
    public Guid Id { get; set; }
    public required string ProjectName { get; set; }
    public string Description { get; set; } = "";
    public Category Category { get; set; } = Category.Other;
    public ItemStatus ItemStatus { get; set; } = ItemStatus.FullyFinished;

    public required string ImageUrl { get; set; }
    // Optional URLs
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }


    // nav properties (for entity framework)
    // Item can exist independently of Auction, but auction needs an Item
    public Auction? Auction { get; set; }
    public Guid? AuctionId { get; set; }
}


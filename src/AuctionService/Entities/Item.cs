using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

[Table("Items")]

public class Item
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string Description { get; set; } = "";
    public Category Category { get; set; }
    public ItemStatus ItemStatus { get; set; } = ItemStatus.FullyFinished;

    // Optional URLs
    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }
    public string? ImageUrl { get; set; }


    // nav properties (for entity framework)
    public Auction? Auction { get; set; }
    public Guid? AuctionId { get; set; }
}


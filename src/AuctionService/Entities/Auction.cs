using System;

namespace AuctionService.Entities;

public class Auction
{
    public required Guid Id { get; set; } // default primary key
    public int ReservePrice { get; set; } = 0;

    // Optional Seller and Winner fields
    public required string Seller { get; set; }

    public string? Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Standard time format, enforced by Postgres
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AuctionEnd { get; set; }
    public required Status Status { get; set; }
    public required Item Item { get; set; }
}

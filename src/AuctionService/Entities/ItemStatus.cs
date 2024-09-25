namespace AuctionService.Entities;

public enum ItemStatus
{
    Idea,
    InDevelopment,   // Actively being worked on, but incomplete
    MVP,             // Minimum Viable Product – functional but limited features
    PartiallyFinished, // More than MVP but not fully complete
    FullyFinished,   // Fully completed and feature-rich
    Maintenance      // Project is complete but may receive bug fixes or updates
}

using System;
using System.ComponentModel.DataAnnotations;
using AuctionService.Entities;

namespace AuctionService.DTOs;

public class CreateAuctionDto
{
    [Required]
    public required string ProjectName { get; set; }

    public string Description { get; set; } = "";

    [Required]
    public required string Category { get; set; }

    [Required]
    public required string ItemStatus { get; set; }


    public string? RepositoryUrl { get; set; }
    public string? DemoUrl { get; set; }
    public string? DocumentationUrl { get; set; }

    [Required]
    public required string ImageUrl { get; set; }

    [Required]
    public int ReservePrice { get; set; }
    public DateTime AuctionEnd { get; set; }
}

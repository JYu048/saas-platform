using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DbInitializer
{
    public static void InitDB(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<AuctionDbContext>();

        if (context == null)
        {
            Console.WriteLine("AuctionDbContext is null, cannot seed data.");
            return;
        }

        SeedData(context);
    }

    private static void SeedData(AuctionDbContext context)
    {
        // Apply pending migrations, ensuring db is up to date
        context.Database.Migrate();

        if (context.Auctions.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var auctions = new List<Auction>(){
                // Auction 1: SaaS project
                new Auction
                {
                    Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                    Status = Status.Live,
                    ReservePrice = 5000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Item = new Item
                    {
                        ProjectName = "Team Collaboration Tool",
                        Description = "A SaaS platform for teams to manage projects, chat, and collaborate in real time.",
                        Category = Category.SaaS,
                        ItemStatus = ItemStatus.MVP,
                        RepositoryUrl = "https://github.com/sample/collab-tool",
                        DemoUrl = "https://collabtool.com/demo",
                        DocumentationUrl = "https://docs.collabtool.com",
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/08/07/22/57/technology-2619526_960_720.jpg"
                    }
                },
                // Auction 2: AI project
                new Auction
                {
                    Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                    Status = Status.Live,
                    ReservePrice = 12000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(20),
                    Item = new Item
                    {
                        ProjectName = "AI Image Classifier",
                        Description = "An AI-powered tool for recognizing and classifying images into categories.",
                        Category = Category.AI,
                        ItemStatus = ItemStatus.PartiallyFinished,
                        RepositoryUrl = "https://github.com/sample/image-classifier",
                        DemoUrl = "https://imageclassifier.ai/demo",
                        DocumentationUrl = "https://docs.imageclassifier.ai",
                        ImageUrl = "https://cdn.pixabay.com/photo/2018/03/02/19/14/robot-3190924_960_720.jpg"
                    }
                },
                // Auction 3: API project
                new Auction
                {
                    Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                    Status = Status.Live,
                    ReservePrice = 6000,
                    Seller = "charlie",
                    AuctionEnd = DateTime.UtcNow.AddDays(15),
                    Item = new Item
                    {
                        ProjectName = "Weather API",
                        Description = "A RESTful API providing accurate and real-time weather data for any location.",
                        Category = Category.API,
                        ItemStatus = ItemStatus.FullyFinished,
                        RepositoryUrl = "https://github.com/sample/weather-api",
                        DemoUrl = "https://weatherapi.com/demo",
                        DocumentationUrl = "https://docs.weatherapi.com",
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/08/10/07/32/clouds-2617750_960_720.jpg"
                    }
                },
                // Auction 4: Productivity tool
                new Auction
                {
                    Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                    Status = Status.ReserveNotMet,
                    ReservePrice = 8000,
                    Seller = "dave",
                    AuctionEnd = DateTime.UtcNow.AddDays(-5),
                    Item = new Item
                    {
                        ProjectName = "Time Tracker",
                        Description = "A web-based tool for tracking productivity and managing daily tasks efficiently.",
                        Category = Category.Productivity,
                        ItemStatus = ItemStatus.MVP,
                        RepositoryUrl = "https://github.com/sample/timetracker",
                        DemoUrl = "https://timetracker.com/demo",
                        DocumentationUrl = "https://docs.timetracker.com",
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/11/23/15/24/time-1858504_960_720.jpg"
                    }
                },
                // Auction 5: Web service
                new Auction
                {
                    Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                    Status = Status.Live,
                    ReservePrice = 10000,
                    Seller = "emma",
                    AuctionEnd = DateTime.UtcNow.AddDays(25),
                    Item = new Item
                    {
                        ProjectName = "E-commerce Web App",
                        Description = "A complete e-commerce platform with a shopping cart, payment gateway, and product catalog.",
                        Category = Category.WebServices,
                        ItemStatus = ItemStatus.PartiallyFinished,
                        RepositoryUrl = "https://github.com/sample/ecommerce-app",
                        DemoUrl = "https://ecommerceapp.com/demo",
                        DocumentationUrl = "https://docs.ecommerceapp.com",
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/10/13/09/06/shop-1732305_960_720.jpg"
                    }
                }
            };

        // Add seed data to the context
        context.AddRange(auctions);
        context.SaveChanges();
    }
}



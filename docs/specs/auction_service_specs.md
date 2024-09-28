# Auction Service Specification

- Initial Draft

## Infrastructure

- **.Net Web API**
- **Postgres DB**
- **Entity Framework ORM**
- **Service Bus - RabbitMQ**

## NuGet Packages

- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- `MassTransit.RabbitMQ`

## External (User)

- **CreateAuction** - Creates a software project auction. Emits `AuctionCreated`.
- **UpdateAuction** - Updates an auction. Emits `AuctionUpdated`.
- **DeleteAuction** - Deletes an auction if state allows (no bids on auction, reserve not met). Emits `AuctionDeleted`.

## Queries Handled

- **GetAuctionById** - Gets an auction by an ID. Returns `AuctionDto`.
- **GetAuctions** - Gets all auctions. Returns a list of `AuctionDto`.
- **GetAuctionsFromDate** - Gets all auctions updated from a specific date.

## Events Emitted

- **AuctionCreated** - When the auction is created in response to `CreateAuction`. Emits `AuctionDto`.
- **AuctionUpdated** - When the auction is updated in response to `UpdateAuction`. Emits `AuctionDto`.
- **AuctionDeleted** - When the auction is deleted in response to `DeleteAuction`. Emits `AuctionId`.

## Events Consumed

- **BidService.BidPlaced** - When a bid has been placed in the BidService.
- **BidService.BiddingFinished** - When an auction has reached the `AuctionEnd` date.

## API Endpoints

- **POST** `api/auctions` - Create a software project auction. (Auth required)
- **PUT** `api/auctions/:id` - Update auction by ID. (Auth required)
- **DELETE** `api/auctions/:id` - Delete auction by ID. (Auth required)
- **GET** `api/auctions` - Get all auctions. (Anonymous)
- **GET** `api/auctions/:id` - Get auction by ID. (Anonymous)

## Models

### Auction.cs

| Property Name   | Property Type | Default Value         |
| --------------- | ------------- | --------------------- |
| Id              | Guid          |                       |
| ReservePrice    | int           | 0                     |
| Seller          | string        | (username from claim) |
| Winner?         | string        | (username of winner)  |
| SoldAmount?     | int           |                       |
| CurrentHighBid? | int           |                       |
| CreatedAt       | DateTime      | DateTime.UtcNow       |
| UpdatedAt       | DateTime      | DateTime.UtcNow       |
| AuctionEnd?     | DateTime      |                       |
| Status          | Status        | Status.Live           |
| Item            | Item          |                       |

### Item.cs

| Property Name     | Property Type     | Default Value            |
| ----------------- | ----------------- | ------------------------ |
| Id                | Guid              |                          |
| ProjectName       | string            |                          |
| Description       | string            |                          |
| Category          | Category          | Category.SaaS            |
| ItemStatus        | ItemStatus        | ItemStatus.FullyFinished |
| RepositoryUrl?    | string            |                          |
| DemoUrl?          | string            |                          |
| DocumentationUrl? | string            |                          |
| ImageUrl?         | string            |                          |
| Auction           | Auction (related) |                          |
| AuctionId         | Guid              |                          |

### Status.cs (enum)

| Status Name   |
| ------------- |
| Live          |
| Finished      |
| ReserveNotMet |

### Category.cs (enum)

| Category Name |
| ------------- |
| SaaS          |
| AI            |
| Productivity  |
| WebService    |
| API           |
| Entertainment |
| Other         |

## DTOs

### AuctionDto.cs

| Property Name    | Property Type |
| ---------------- | ------------- |
| Id               | Guid          |
| CreatedAt        | DateTime      |
| UpdatedAt        | DateTime      |
| AuctionEnd       | DateTime      |
| Seller           | string        |
| Winner           | string        |
| Status           | string        |
| ReservePrice     | int           |
| SoldAmount       | int           |
| CurrentHighBid   | int           |
| ProjectName      | string        |
| Description      | string        |
| Category         | string        |
| ItemStatus       | string        |
| RepositoryUrl    | string        |
| DemoUrl          | string        |
| DocumentationUrl | string        |
| ImageUrl         | string        |

### CreateAuctionDto.cs

| Property Name    | Property Type |
| ---------------- | ------------- |
| ProjectName      | string        |
| Description      | string        |
| Category         | string        |
| ItemStatus       | string        |
| RepositoryUrl    | string        |
| DemoUrl          | string        |
| DocumentationUrl | string        |
| ImageUrl         | string        |
| ReservePrice     | int           |
| AuctionEnd       | DateTime      |

### UpdateAuctionDto.cs

| Property Name    | Property Type |
| ---------------- | ------------- |
| ProjectName      | string        |
| Description      | string        |
| Category         | string        |
| ItemStatus       | string        |
| RepositoryUrl    | string        |
| DemoUrl          | string        |
| DocumentationUrl | string        |
| ImageUrl         | string        |
| AuctionEnd       | DateTime      |

## Event Types

### AuctionCreated

| Property Name     | Property Type | Description                                        |
| ----------------- | ------------- | -------------------------------------------------- |
| Id                | Guid          | Unique identifier for the auction                  |
| CreatedAt         | DateTime      | The time the auction was created                   |
| UpdatedAt         | DateTime      | The last time the auction was updated              |
| AuctionEnd?       | DateTime      | When the auction is set to end (nullable)          |
| Seller            | string        | The username of the person who created the auction |
| Winner?           | string        | The username of the auction winner (nullable)      |
| ProjectName       | string        | The name of the software project                   |
| Description       | string        | A description of the software project              |
| Category          | Category      | The category (e.g., SaaS, AI, API, etc.)           |
| ItemStatus        | ItemStatus    |                                                    |
| RepositoryUrl?    | string        | The URL to the project's repository (nullable)     |
| DemoUrl?          | string        | The URL to the project demo (nullable)             |
| DocumentationUrl? | string        | The URL to the project documentation (nullable)    |
| ImageUrl?         | string        | URL for an image of the project (nullable)         |
| Status            | Status        | The current status of the auction                  |
| ReservePrice      | int           | The minimum price required to sell                 |
| SoldAmount?       | int           | The amount the auction was sold for (nullable)     |
| CurrentHighBid?   | int           | The current highest bid (nullable)                 |

### AuctionUpdated

| Property Name     | Property Type | Description                                     |
| ----------------- | ------------- | ----------------------------------------------- |
| Id                | Guid          | Unique identifier for the auction               |
| ProjectName       | string        | The name of the software project                |
| Description       | string        | A description of the software project           |
| Category          | Category      | The category (e.g., SaaS, AI, API, etc.)        |
| ItemStatus        | ItemStatus    |                                                 |
| RepositoryUrl?    | string        | The URL to the project's repository (nullable)  |
| DemoUrl?          | string        | The URL to the project demo (nullable)          |
| DocumentationUrl? | string        | The URL to the project documentation (nullable) |
| ImageUrl?         | string        |                                                 |
| AuctionEnd?       | DateTime      |                                                 |

### AuctionDeleted

| Property Name | Property Type | Description                       |
| ------------- | ------------- | --------------------------------- |
| Id            | Guid          | Unique identifier for the auction |

## Events Consumed Types

### BidService.BidPlaced

| Property Name     | Property Type | Description                                     |
| ----------------- | ------------- | ----------------------------------------------- |
| Id                | Guid          | Unique identifier for the auction               |
| ProjectName       | string        | The name of the software project                |
| Description       | string        | A description of the software project           |
| Category          | Category      | The category (e.g., SaaS, AI, API, etc.)        |
| RepositoryUrl?    | string        | The URL to the project's repository (nullable)  |
| DemoUrl?          | string        | The URL to the project demo (nullable)          |
| DocumentationUrl? | string        | The URL to the project documentation (nullable) |
| ImageUrl?         | string        | URL for an image of the project (nullable)      |
| ReservePrice      | int           | The minimum price required to sell              |
| AuctionEnd?       | DateTime      | When the auction is set to end (nullable)       |

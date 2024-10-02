# Search Service Specification

## Infrastructure

- **.NET Web API**
- **MongoDB** - Database for storing auctions and search-related data.
- **Service Bus** - RabbitMQ

## NuGet Packages

- `AutoMapper.Extensions.Microsoft.DependancyInjection`
- `Microsoft.Extensions.Http.Polly`
- `MongoDB.Entities`
- `MassTransit.RabbitMQ`

## Queries Handled

- **Search**: Gets a paged list of auctions based on query parameters.

## Events Consumed

- **AuctionService.AuctionCreated**: When an auction is created in the AuctionService.
- **AuctionService.AuctionUpdated**: When an auction is updated in the AuctionService.
- **AuctionService.AuctionDeleted**: When an auction is deleted in the AuctionService.
- **BidService.AuctionFinished**: When an auction has reached its `AuctionEnd` date/time.
- **BidService.BidPlaced**: When a bid is placed in the BidService.

## API Endpoints

- **GET `/api/search`**
  - Get a paged list of auctions based on query parameters.
  - **Query Parameters**:
    - `searchTerm`
    - `pageSize`
    - `pageNumber`
    - `seller`
    - `winner`
    - `orderBy`
    - `filterBy`
  - **Access Level**: Anonymous

## Models

### Item.cs

| Property Name     | Property Type | Default Value            |
| ----------------- | ------------- | ------------------------ |
| Id                | Guid          |                          |
| CreatedAt         | DateTime      |                          |
| UpdatedAt         | DateTime      |                          |
| AuctionEnd?       | DateTime      |                          |
| ProjectName       | string        |                          |
| Description       | string        |                          |
| Category          | Category      |                          |
| ItemStatus        | ItemStatus    | ItemStatus.FullyFinished |
| RepositoryUrl?    | string        |                          |
| DemoUrl?          | string        |                          |
| DocumentationUrl? | string        |                          |
| ImageUrl?         | string        |                          |
| CreatedAt         | DateTime      |                          |
| UpdatedAt         | DateTime      |                          |
| Seller            | string        |                          |
| Winner?           | string        |                          |
| ReservePrice      | int           |                          |
| SoldAmount?       | int           |                          |
| CurrentHighBid?   | int           |                          |

## Events Consumed Types

### AuctionCreated

| Property Name     | Property Type |
| ----------------- | ------------- |
| Id                | Guid          |
| CreatedAt         | DateTime      |
| UpdatedAt         | DateTime      |
| AuctionEnd?       | DateTime      |
| Seller            | string        |
| Winner?           | string        |
| ProjectName       | string        |
| Description       | string        |
| Category          | string        |
| ItemStatus        | string        |
| RepositoryUrl?    | string        |
| DemoUrl?          | string        |
| DocumentationUrl? | string        |
| ImageUrl?         | string        |
| Status            | string        |
| ReservePrice      | int           |
| SoldAmount?       | int           |
| CurrentHighBid?   | int           |

### AuctionUpdated

| Property Name     | Property Type |
| ----------------- | ------------- |
| ProjectName       | string        |
| Description       | string        |
| Category          | string        |
| ItemStatus        | string        |
| RepositoryUrl?    | string        |
| DemoUrl?          | string        |
| DocumentationUrl? | string        |
| ImageUrl?         | string        |
| AuctionEnd?       | DateTime      |

### AuctionDeleted

| Property Name | Property Type |
| ------------- | ------------- |
| Id            | Guid          |

### AuctionFinished

| Property Name | Property Type |
| ------------- | ------------- |
| AuctionId     | Guid          |
| ItemSOld      | Boolean       |
| Winner        | string        |
| Seller        | string        |
| Amount?       | int           |

### BidPlaced

| Property Name | Property Type |
| ------------- | ------------- |
| Id            | Guid          |
| AuctionId     | Guid          |
| Bidder        | string        |
| BidTime       | DateTime      |
| Amount        | int           |
| BidStatus     | string        |

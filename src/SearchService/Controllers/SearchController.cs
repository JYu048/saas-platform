using System;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Search;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> SearchItems([FromQuery] string searchTerm, int pageNumber = 1, int pageSize = 4)
    {
        var query = DB.PagedSearch<Item>();

        query.Sort(a => a.ProjectName, Order.Ascending);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            // TODO: sort by make first
            query.Match(Search.Full, searchTerm).SortByTextScore();
        }

        query.PageNumber(pageNumber);
        query.PageSize(pageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            result = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });

    }
}

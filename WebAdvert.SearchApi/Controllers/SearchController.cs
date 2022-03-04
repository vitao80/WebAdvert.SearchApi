using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchApi.Models;
using WebAdvert.SearchApi.Services;

namespace WebAdvert.SearchApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("search/v1")]
    [ApiController]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> looger)
        {
            _searchService = searchService;
            _logger = looger;
        }

        [HttpGet]
        [Route("{keyword}")]
        public async Task<List<AdvertType>> Get(string keyword)
        {
            _logger.LogInformation($"Search method was called. keyword: {keyword}");
            return await _searchService.Search(keyword);
        }
    }
}

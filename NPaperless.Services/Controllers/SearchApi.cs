using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPaperless.Services.DTOs;
using System;
using System.Collections.Generic;
using NPaperless.SearchLibrary;

namespace NPaperless.Services.Controllers
{ 
    [ApiController]
    public class SearchApiController : ControllerBase
    { 
        private readonly ILogger<SearchApiController> _logger;
        private readonly ElasticSearchIndex _elasticSearchIndex;

        public SearchApiController(ILogger<SearchApiController> logger, ElasticSearchIndex elasticSearchIndex)
        {
            _logger = logger;
            _elasticSearchIndex = elasticSearchIndex;
        }

        [HttpGet]
        [Route("/api/search/autocomplete")]
        public IActionResult AutoComplete([FromQuery(Name = "term")]string term, [FromQuery(Name = "limit")]int? limit)
        {
            _logger.LogInformation($"AutoComplete search for term: {term} with limit: {limit}");

            try
            {
                // Simulate search operation
                List<string> results = PerformSearch(term, limit);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while performing search.");
                return StatusCode(500, "An error occurred while performing search.");
            }
        }

        // Simulate a search function
        private List<string> PerformSearch(string term, int? limit)
        {
            var results = _elasticSearchIndex.SearchDocumentAsync(term, limit ?? 10);
            return results.Select(x => x.Title).ToList();
        }
    }
}
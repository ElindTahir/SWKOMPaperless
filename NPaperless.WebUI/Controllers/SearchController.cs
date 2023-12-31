using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NPaperless.WebUI.Controllers
{
    [ApiController]
    [Route("/api/search/")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly HttpClient _httpClient;

        public SearchController(ILogger<SearchController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet("autocomplete/", Name = "AutoComplete")]
        public async Task<IActionResult> AutoComplete([FromQuery]string term, [FromQuery]int limit = 10)
        {
            var response = await _httpClient.GetAsync($"api/search/autocomplete?term={term}&limit={limit}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var autoCompleteResults = JsonSerializer.Deserialize<List<string>>(content);
                return Ok(autoCompleteResults);
            }
            else
            {
                _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NPaperless.WebUI.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace NPaperless.WebUI.Controllers;

[ApiController]
[Route("/api/correspondents")]
public class CorrespondentsController : ControllerBase
{
    private readonly ILogger<CorrespondentsController> _logger;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public CorrespondentsController(IMapper mapper, ILogger<CorrespondentsController> logger, IHttpClientFactory httpClientFactory)
    {
        _mapper = mapper;
        _logger = logger;
        //_httpClient = httpClientFactory.CreateClient("NPaperlessAPI");
        _httpClient = httpClientFactory.CreateClient("ServiceClient");
        //_httpClient.BaseAddress = new Uri("http://npaperless.services:8081/");
    }
    
    [HttpGet(Name = "GetCorrespondents")]
    public async Task<IActionResult> GetCorrespondents()
    {
        var response = await _httpClient.GetAsync("api/correspondents");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var correspondents = JsonSerializer.Deserialize<List<Correspondent>>(content);
            if (correspondents == null || correspondents.Count == 0)
            {
                return Ok(Builder<ListResponse<Correspondent>>.CreateNew()
                    .With(x => x.Count = 0)
                    .With(x => x.Next = null)
                    .With(x => x.Previous = null)
                    .With(x => x.Results = ImmutableList<Correspondent>.Empty)
                    .Build());
            }

            return Ok(Builder<ListResponse<Correspondent>>.CreateNew()
                .With(x => x.Count = correspondents.Count)
                .With(x => x.Next = null)
                .With(x => x.Previous = null)
                .With(x => x.Results = correspondents)
                .Build());
        }
        else
        {
            _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }

    [HttpPost(Name = "CreateCorrespondent")]
    public async Task<IActionResult> CreateCorrespondent([FromBody] NewCorrespondent correspondent)
    {
        var content = new StringContent(JsonSerializer.Serialize(correspondent), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/correspondents", content);
        if (response.IsSuccessStatusCode)
        {
            var readContent = await response.Content.ReadAsStringAsync();
            var createdCorrespondent = JsonSerializer.Deserialize<Correspondent>(readContent);
            _logger.LogInformation($"Created Correspondent: {createdCorrespondent.Id}");
            return Created($"/api/correspondents/{createdCorrespondent.Id}", createdCorrespondent);
        }
        else
        {
            _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }

    [HttpPut("{id:int}", Name = "UpdateCorrespondent")]
    public async Task<IActionResult> UpdateCorrespondent([FromRoute] int id, [FromBody] Correspondent correspondent)
    {
        var content = new StringContent(JsonSerializer.Serialize(correspondent), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/correspondents/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            var readContent = await response.Content.ReadAsStringAsync();
            var updatedCorrespondent = JsonSerializer.Deserialize<Correspondent>(readContent);
            _logger.LogInformation($"Updated Correspondent: {updatedCorrespondent.Id}");
            return Accepted($"/api/correspondents/{updatedCorrespondent.Id}", updatedCorrespondent);
        }
        else
        {
            _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }

    [HttpDelete("{id:int}", Name = "DeleteCorrespondent")]
    public async Task<IActionResult> DeleteCorrespondent([FromRoute] int id)
    {
        var response = await _httpClient.DeleteAsync($"api/correspondents/{id}");
        if (response.IsSuccessStatusCode)
        {
            return NoContent();
        }
        else
        {
            _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NPaperless.WebUI.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NPaperless.WebUI.Controllers;

[ApiController]
[Route("/api/correspondents/")]
public class CorrespondentsController : ControllerBase
{
    private readonly ILogger<CorrespondentsController> _logger;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public CorrespondentsController(IMapper mapper, ILogger<CorrespondentsController> logger, HttpClient httpClient)
    {
        _mapper = mapper;
        _logger = logger;
        //_httpClient = httpClientFactory.CreateClient("NPaperlessAPI");
        _httpClient = httpClient;
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
            return Ok(correspondents);
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
            return Ok(correspondent);
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

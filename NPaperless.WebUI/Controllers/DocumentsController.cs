using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Document = NPaperless.WebUI.Models.Document;

namespace NPaperless.WebUI.Controllers
{
    [ApiController]
    [Route("/api/documents/")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly HttpClient _httpClient;

        public DocumentsController(ILogger<DocumentsController> logger, HttpClient httpClient)
        {
            _logger = logger;
            //_httpClient = httpClientFactory.CreateClient("NPaperlessAPI"); // Ensure this client is configured in Startup.cs
            _httpClient = httpClient;
            //Diese Zeile ist eine missgeburt
            //_httpClient.BaseAddress = new Uri("http://npaperless.services:8081/");
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            _logger.LogInformation("Fetching documents from API");
            var response = await _httpClient.GetAsync("api/documents");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var documents = JsonSerializer.Deserialize<List<Document>>(content);
                return Ok(documents);
            }
            else
            {
                _logger.LogError($"Error fetching documents: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Document newDocument)
        {
            _logger.LogInformation("Creating a new document: {Title}", newDocument.Title);
            var jsonContent = JsonSerializer.Serialize(newDocument);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/documents", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var readContent = await response.Content.ReadAsStringAsync();
                var createdDocument = JsonSerializer.Deserialize<Document>(readContent);
                return CreatedAtAction(nameof(GetDocument), new { id = createdDocument.Id }, createdDocument);
            }
            else
            {
                _logger.LogError($"Error creating document: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            _logger.LogInformation("Fetching document with ID: {Id}", id);
            var response = await _httpClient.GetAsync($"api/documents/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var document = JsonSerializer.Deserialize<Document>(content);
                return Ok(document);
            }
            else
            {
                _logger.LogError($"Error fetching document with ID {id}: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document updateDocument)
        {
            _logger.LogInformation("Updating document with ID: {Id}", id);
            var jsonContent = JsonSerializer.Serialize(updateDocument);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/documents/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error updating document with ID {id}: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            _logger.LogInformation("Deleting document with ID: {Id}", id);
            var response = await _httpClient.DeleteAsync($"api/documents/{id}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                _logger.LogError($"Error deleting document with ID {id}: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        // Add any other actions your controller should handle, following similar patterns for making requests
        // and handling responses from your REST API.

        // ...
    }
}

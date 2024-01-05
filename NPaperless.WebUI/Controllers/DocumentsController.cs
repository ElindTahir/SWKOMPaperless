using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using NPaperless.Services.DTOs;
using NPaperless.Services.MinIO;
using Document = NPaperless.WebUI.Models.Document;
using NPaperless.QueueLibrary;

namespace NPaperless.WebUI.Controllers
{
    [ApiController]
    [Route("/api/documents/")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly HttpClient _httpClient;
        private FileUpload _fileUpload;
        private IQueueProducer _queueProducer;

        public DocumentsController(ILogger<DocumentsController> logger, HttpClient httpClient, FileUpload fileUpload, 
            IHttpClientFactory httpClientFactory, IQueueProducer queueProducer)
        {
            _logger = logger;
            //_httpClient = httpClientFactory.CreateClient("NPaperlessAPI"); // Ensure this client is configured in Startup.cs
            _httpClient = httpClient;
            //Diese Zeile ist eine missgeburt
            //_httpClient.BaseAddress = new Uri("http://npaperless.services:8081/");
            _fileUpload = fileUpload;
            _httpClient = httpClientFactory.CreateClient("ServiceClient");
            _queueProducer = queueProducer;
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

        [HttpPost("post_document", Name = "UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] string? title,
            [FromForm] DateTime? created,
            [FromForm(Name = "document_type")] uint? documentType,
            [FromForm] uint[] tags,
            [FromForm] uint? correspondent,
            [FromForm] IEnumerable<IFormFile> document)
        {

            // Get Document content
            var file = document.FirstOrDefault();
            if (file == null)
            {
                return BadRequest("No file was uploaded");
            }

            // Get file content 
            var fileContent = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(fileContent);
            
            
            // Upload file to MinIO
            await _fileUpload.UploadFileAsync(file.OpenReadStream(), file.FileName);

            // Create Document object
            var doc = new NPaperless.WebUI.Models.Document()
            {
                
                Correspondent = null,
                DocumentType = 0,
                StoragePath = 0,
                Title = file.FileName,
                Content = "Default Content",
                //empty tags array
                Tags = new uint[0],
                CreatedDate = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                Added = DateTime.UtcNow,
                ArchiveSerialNumber = "test",
                OriginalFileName = file.FileName,
                ArchivedFileName = "title"
            };

            var response = await _httpClient.PostAsJsonAsync("api/documents", doc);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var createdDocument = JsonSerializer.Deserialize<Document>(content);
                
                Guid documentGuid = Guid.NewGuid();
                _queueProducer.Send(file.FileName, documentGuid);
                
                _logger.LogInformation($"Created Document: {createdDocument.Id}");
                return Created($"/api/documents/{createdDocument.Id}", createdDocument);
                
            }
            else
            {
                _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
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

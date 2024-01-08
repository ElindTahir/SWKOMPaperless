using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NPaperless.WebUI.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NPaperless.WebUI.Controllers
{
    [ApiController]
    [Route("/api/document_types/")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly ILogger<DocumentTypesController> _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public DocumentTypesController(IMapper mapper, ILogger<DocumentTypesController> logger, HttpClient httpClient)
        {
            _mapper = mapper;
            _logger = logger;
            _httpClient = httpClient;
            
        }

        [HttpGet(Name = "GetDocumentTypes")]
        public async Task<IActionResult> GetDocumentTypes()
        {
            /*var response = await _httpClient.GetAsync("api/document_types");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var documentTypes = JsonSerializer.Deserialize<List<DocumentType>>(content);
                return Ok(documentTypes);
            }
            else
            {
                _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }*/
            var resultObject = new
            {
                count = 0,
                next = (string)null, // Update as needed
                previous = (string)null, // Update as needed
                // all is empty List<int> because we don't have a way to get all document types
                all = new List<int>(),
                results = new List<DocumentType>()
            };
            return Ok(resultObject);
        }

        [HttpPost(Name = "CreateDocumentType")]
        public async Task<IActionResult> CreateDocumentType([FromBody] NewDocumentType documentType)
        {
            var content = new StringContent(JsonSerializer.Serialize(documentType), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/document_types", content);
            if (response.IsSuccessStatusCode)
            {
                var readContent = await response.Content.ReadAsStringAsync();
                var createdDocumentType = JsonSerializer.Deserialize<DocumentType>(readContent);
                return Created($"/api/document_types/{createdDocumentType.Id}", createdDocumentType);
            }
            else
            {
                _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateDocumentType")]
        public async Task<IActionResult> UpdateDocumentType([FromRoute] int id, [FromBody] DocumentType documentType)
        {
            var content = new StringContent(JsonSerializer.Serialize(documentType), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/document_types/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return Ok(documentType);
            }
            else
            {
                _logger.LogError($"Error calling REST API: {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteDocumentType")]
        public async Task<IActionResult> DeleteDocumentType([FromRoute] int id)
        {
            var response = await _httpClient.DeleteAsync($"api/document_types/{id}");
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
}

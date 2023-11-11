using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPaperless.Services.DTOs;


namespace NPaperless.Services.Controllers
{
    [ApiController]
    public class DocumentsApiController : ControllerBase
    {
        private readonly ILogger<DocumentsApiController> _logger;

        public DocumentsApiController(ILogger<DocumentsApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/documents")]
        public IActionResult UploadDocument([FromBody]DocumentsDTO.NewDocument newDocument)
        {
            _logger.LogInformation("Received request to upload document: {@NewDocument}", newDocument);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents/{id}")]
        public IActionResult GetDocument([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request to get document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/api/documents/{id}")]
        public IActionResult DeleteDocument([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request to delete document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents/{id}/download")]
        public IActionResult DownloadDocument([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request to download document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents/{id}/metadata")]
        public IActionResult GetDocumentMetadata([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request for metadata of document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents/{id}/preview")]
        public IActionResult GetDocumentPreview([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request for preview of document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents/{id}/thumb")]
        public IActionResult GetDocumentThumb([FromRoute][Required]int id)
        {
            _logger.LogInformation("Received request for thumbnail of document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/documents")]
        public IActionResult GetDocuments([FromQuery]int? page, [FromQuery]int? pageSize, [FromQuery]string query, [FromQuery]string ordering, [FromQuery(Name = "tags__id__all")]List<int> tagsIdAll, [FromQuery(Name = "document_type__id")]int? documentTypeId, [FromQuery(Name = "correspondent__id")]int? correspondentId, [FromQuery]bool? truncateContent)
        {
            _logger.LogInformation("Received request to get documents with query parameters.");
            // Your logic here
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/api/documents/{id}")]
        public IActionResult UpdateDocument([FromRoute][Required]int id, [FromBody]DocumentsDTO.UpdateDocument document)
        {
            _logger.LogInformation("Received request to update document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }
    }
}

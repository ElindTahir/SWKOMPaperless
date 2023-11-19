using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using NPaperless.DataAccess.Sql;
using NPaperless.Services.DTOs;



namespace NPaperless.Services.Controllers
{
    [ApiController]
    public class DocumentsApi : ControllerBase
    {
        private readonly ILogger<DocumentsApi> _logger;
        private DocumentRepository _documentRepository;
        private NPaperlessDbContext _dbContext;
        private IMapper _mapper;

        public DocumentsApi(ILogger<DocumentsApi> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            var connectionString =
                "Host=npaperless.dataaccess.sql;Database=npaperless;Username=npaperless;Password=npaperless;";
            var optionsBuilder = new DbContextOptionsBuilder<NPaperlessDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            _dbContext = new NPaperlessDbContext(optionsBuilder.Options);
            _documentRepository = new DocumentRepository(_dbContext);
        }

        [HttpPost]
        [Route("/api/documents")]
        public IActionResult UploadDocument([FromBody]DocumentDTO newDocument)
        {
            var createdDocument = _documentRepository.Add(_mapper.Map<DataAccess.Entities.Document>(newDocument));
            
            if (createdDocument.Id > 0)
            {
                return Created($"/api/documents/{createdDocument.Id}", createdDocument);
            }
            else
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet]
        [Route("/api/documents")]
        public IActionResult GetDocuments([FromQuery]int? page, [FromQuery]int? page_size, [FromQuery]string? query, [FromQuery]string ordering, [FromQuery(Name = "tags__id__all")]List<int>? tagsIdAll, [FromQuery(Name = "document_type__id")]int? documentTypeId, [FromQuery(Name = "correspondent__id")]int? correspondent__id, [FromQuery]bool? truncate_content)
        {
            _logger.LogInformation("Received request to get documents with query parameters.");
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

        [HttpPut]
        [Route("/api/documents/{id}")]
        public IActionResult UpdateDocument([FromRoute][Required]int id, [FromBody]DocumentDTO document)
        {
            _logger.LogInformation("Received request to update document with ID: {Id}", id);
            // Your logic here
            throw new NotImplementedException();
        }
    }
}

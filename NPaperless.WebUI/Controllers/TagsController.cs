using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NPaperless.WebUI.Models;
using System;

namespace NPaperless.WebUI.Controllers
{
    [ApiController]
    [Route("/api/tags/")]
    public class TagsController : ControllerBase
    {
        private readonly ILogger<TagsController> _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public TagsController(IMapper mapper, ILogger<TagsController> logger, HttpClient httpClient)
        {
            _mapper = mapper;
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet(Name = "GetTags")]
        public IActionResult GetTags()
        {
            _logger.LogInformation("Retrieving tags.");
            // Logic to get tags would go here
            return Ok(); // Assuming retrieval is successful
        }

        [HttpPost(Name = "CreateTag")]
        public IActionResult CreateTag([FromBody] NewTag newTag)
        {
            var tag = _mapper.Map<Tag>(newTag);
            _logger.LogInformation("Creating tag: {@Tag}", tag);
            // Logic to create a new tag would go here
            return Created($"/api/tags/{tag.Id}", tag); // Assuming creation is successful
        }

        [HttpPut("{id:int}", Name = "UpdateTag")]
        public IActionResult UpdateTag([FromRoute] int id, [FromBody] Tag tag)
        {
            _logger.LogInformation("Updating tag with ID: {Id}: {@Tag}", id, tag);
            // Logic to update a tag would go here
            return Ok(tag); // Assuming update is successful
        }

        [HttpDelete("{id:int}", Name = "DeleteTag")]
        public IActionResult DeleteTag([FromRoute] int id)
        {
            _logger.LogInformation("Deleting tag with ID: {Id}", id);
            // Logic to delete a tag would go here
            return NoContent(); // Assuming deletion is successful
        }
    }
}
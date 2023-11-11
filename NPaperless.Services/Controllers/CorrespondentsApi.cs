using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPaperless.Services.DTOs;
//using NPaperless.DataAccess.Sql;

namespace NPaperless.Services.Controllers
{
    [ApiController]
    public class CorrespondentsApiController : ControllerBase
    {
        private readonly ILogger<CorrespondentsApiController> _logger;
        //private readonly IRepository<NewCorrespondent> _correspondentRepository;
        public CorrespondentsApiController(ILogger<CorrespondentsApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/api/correspondents")]
        public virtual IActionResult CreateCorrespondent([FromBody]NewCorrespondent newCorrespondent)
        {
            /*_logger.LogInformation("Creating a new correspondent: {@NewCorrespondent}", newCorrespondent);
            // Simulate some work
            _logger.LogInformation("Correspondent created successfully.");
            return StatusCode(200);*/
            
            
            _logger.LogInformation("Creating a new correspondent: {@NewCorrespondent}", newCorrespondent);
            // using SQL Repository
            //_correspondentRepository.Add(newCorrespondent);
            return StatusCode(200);
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/api/correspondents/{id}")]
        public virtual IActionResult DeleteCorrespondent([FromRoute][Required]int id)
        {
            _logger.LogInformation("Deleting correspondent with ID: {Id}", id);
            // Simulate some work
            // return StatusCode(200);
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/correspondents")]
        public virtual IActionResult GetCorrespondents()
        {
            _logger.LogInformation("Getting all correspondents.");
            // Simulate some work
            // return StatusCode(200);
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/api/correspondents/{id}")]
        public virtual IActionResult UpdateCorrespondent([FromRoute][Required]int id, [FromBody]Correspondent correspondent)
        {
            _logger.LogInformation("Updating correspondent with ID: {Id}, Data: {@Correspondent}", id, correspondent);
            // Simulate some work
            // return StatusCode(200);
            throw new NotImplementedException();
        }
    }
}

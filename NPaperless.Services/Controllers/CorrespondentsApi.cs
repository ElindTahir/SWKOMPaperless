using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPaperless.Services.DTOs;
using NPaperless.DataAccess.Sql;
using NPaperless.Services.DTOs;
using NPaperless.DataAccess.Sql;
namespace NPaperless.Services.Controllers
{
    [ApiController]
    public class CorrespondentsApiController : ControllerBase
    {
        private readonly ILogger<CorrespondentsApiController> _logger;
        private CorrespondentRepository _correspondentRepository;
        private NPaperlessDbContext _dbContext;
        private IMapper _mapper;
        public CorrespondentsApiController(ILogger<CorrespondentsApiController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            //configer dbcontext provider. npgsql
            var connectionString =
                "Host=npaperless.dataaccess.sql;Database=npaperless;Username=npaperless;Password=npaperless;";
            var optionsBuilder = new DbContextOptionsBuilder<NPaperlessDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            _dbContext = new NPaperlessDbContext(optionsBuilder.Options);
            _correspondentRepository = new CorrespondentRepository(_dbContext);
        }

        [HttpPost]
        [Route("/api/correspondents")]
        public virtual IActionResult CreateCorrespondent([FromBody]NewCorrespondentDTO newCorrespondent)
        {
            var createdCorrespondent =  _correspondentRepository.Add(_mapper.Map<DataAccess.Entities.Correspondent>(newCorrespondent));
            if (createdCorrespondent.Id > 0)
            {
                return Created($"/api/correspondents/{createdCorrespondent.Id}", createdCorrespondent);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("/api/correspondents/{id}")]
        public virtual IActionResult DeleteCorrespondent([FromRoute][Required]int id)
        {
            _correspondentRepository.Delete(_correspondentRepository.FindById(id));
            return StatusCode(204);
        }

        [HttpGet]
        [Route("/api/correspondents")]
        public virtual IActionResult GetCorrespondents()
        {
            var correspondents = _correspondentRepository.GetAll();
            return Ok(_mapper.Map<CorrespondentDTO[]>(correspondents));
        }

        [HttpPut]
        [Route("/api/correspondents/{id}")]
        public virtual IActionResult UpdateCorrespondent([FromRoute][Required]int id, [FromBody]CorrespondentDTO correspondent)
        {
            if (string.IsNullOrEmpty(correspondent.Slug))
            {
                correspondent.Slug = correspondent.Name.Replace(" ", "-").ToLower();
            }
            var createdCorrespondent =  _correspondentRepository.Update(_mapper.Map<DataAccess.Entities.Correspondent>(correspondent));
            if (createdCorrespondent.Id > 0)
            {
                return Accepted($"/api/correspondents/{createdCorrespondent.Id}", createdCorrespondent);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}

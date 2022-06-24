using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : BaseController<Education, EducationRepository, int>
    {
        private readonly EducationRepository educationRepository;
        public IConfiguration _configuration;
        public EducationsController(EducationRepository educationRepository, IConfiguration configuration) : base(educationRepository)
        {
            this.educationRepository = educationRepository;
            this._configuration = configuration;
        }
        [HttpGet("GetUniversityDistribution")]
        public ActionResult GetUniversityDistribution()
        {
            var result = educationRepository.GetUniversityDistribution();
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Success Get Employee University Distribution!!", data = result });
        }
    }
}

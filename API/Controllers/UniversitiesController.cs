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
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private readonly UniversityRepository universityRepository;
        public IConfiguration _configuration;

        public UniversitiesController(UniversityRepository universityRepository, IConfiguration configuration) : base(universityRepository)
        {
            this.universityRepository = universityRepository;
            this._configuration = configuration;
        }
        [HttpGet("GetUniversities")]
        public ActionResult GetUniversities()
        {
            return StatusCode(200, new { status=HttpStatusCode.OK, message = "Success Get Universities", data = universityRepository.GetUniversities() });
        }
    }
}

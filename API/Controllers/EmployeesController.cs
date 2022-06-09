using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            switch (result)
            {
                case -1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Nomor Telepon telah digunakan!!!" });
                case -2:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email telah digunakan!!!" });
                case -3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "University with given UniversityId not found!!!" });
                case -4:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Degree tidak valid!!!" });
                case -5:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Gagal ditambahkan!!!" });
                case -6:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Gender tidak valid!!!" });
                default:
                    if (result <= 0)
                    {
                        return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Data Gagal Ditambahkan" });
                    }
                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Register Berhasil" });
            }

            
        }
        
        [Authorize(Roles = "Director,Manager")]
        [HttpGet("getRegisteredData")]
        public ActionResult GetRegisteredData()
        {
            List<RegisteredDataVM> registeredDatas = employeeRepository.GetRegisteredEmployeeData();

            if(registeredDatas.Count <= 0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "There is no Data"});
            }

            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Success Get Registered Data", data = registeredDatas });
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil!!!");
        }
    
    }
}

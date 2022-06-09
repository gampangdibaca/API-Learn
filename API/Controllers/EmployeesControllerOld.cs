using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class EmployeesControllerOld : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesControllerOld(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {

            IEnumerable<Employee> result = employeeRepository.Get();
            if (result.Count() <= 0)
            {
                return StatusCode(204
                    , new { status = HttpStatusCode.NoContent, message = "Data Karyawan Kosong" });
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            Employee isExist = employeeRepository.Get(employee.NIK);
            if (isExist != null)
            {
                //return Conflict($"Employee with NIK: {employee.NIK} is already exist!!!");

                return StatusCode(409
                    , new { status = HttpStatusCode.Conflict, message = $"Employee with NIK: {employee.NIK} is already exist!!!" });
            }

            int result = employeeRepository.Insert(employee);
            if (result <= 0)
            {
                //return BadRequest("Insert Gagal");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Insert Gagal" });
            }
            //return Ok("Insert Berhasil");

            return StatusCode(200
                , new { status = HttpStatusCode.OK, message = "Insert Berhasil" });
        }

        [HttpGet("{nik}")]
        public ActionResult GetByNik(string nik)
        {
            if (string.IsNullOrWhiteSpace(nik))
            {
                //return BadRequest("Please provide Valid NIK");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Please provide Valid NIK" });
            }

            Employee employee = employeeRepository.Get(nik);

            if(employee == null)
            {
                //return BadRequest($"Employee with NIK: {nik} is not found!!!");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = $"Employee with NIK: {nik} is not found!!!" });
            }
            return Ok(employee);
        }

        [HttpPut("update")]
        public ActionResult Update(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.NIK))
            {
                //return BadRequest("Please provide Valid NIK");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Please provide Valid NIK" });
            }

            if (employeeRepository.Get(employee.NIK) == null)
            {
                //return BadRequest($"Employee with NIK: {employee.NIK} is not found!!!");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = $"Employee with NIK: {employee.NIK} is not found!!!" });
            }

            int result = employeeRepository.Update(employee);
            if (result <= 0)
            {
                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Update Gagal" });
            }
            //return Ok();

            return StatusCode(200
                , new { status = HttpStatusCode.OK, message = "Update Berhasil" });
        }

        [HttpDelete("{nik}")]
        public ActionResult Delete(string nik)
        {
            if (string.IsNullOrWhiteSpace(nik))
            {
                //return BadRequest("Please provide Valid NIK");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Please provide Valid NIK" });
            }

            Employee employee = employeeRepository.Get(nik);

            if (employee == null)
            {
                //return BadRequest($"Employee with NIK: {nik} is not found!!!");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = $"Employee with NIK: {nik} is not found!!!" });
            }

            int result = employeeRepository.Delete(nik);
            if (result <= 0)
            {
                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Delete Gagal" });
            }
            //return Ok();

            return StatusCode(200
                , new { status = HttpStatusCode.OK, message = "Delete Berhasil" });
        }

        [HttpGet("gender/{gender}")]
        public ActionResult GetFirstGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                //return BadRequest("Please provide Valid Gender");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Please provide Valid Gender" });
            }

            Employee employee = employeeRepository.FirstGender(gender);

            if (employee == null)
            {
                //return BadRequest($"Employee with Gender: {gender} is not found!!!");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = $"Employee with Gender: {gender} is not found!!!" });
            }
            return Ok(employee);
        }

        [HttpGet("email/{email}")]
        public ActionResult GetSingleEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                //return BadRequest("Please provide Valid Email");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = "Email not Valid" });
            }

            Employee employee = employeeRepository.SingleEmail(email);

            if (employee == null)
            {
                //return BadRequest($"Employee with Email: {email} is not found!!!");

                return StatusCode(400
                    , new { status = HttpStatusCode.BadRequest, message = $"Employee with Email: {email} is not found!!!" });
            }
            return Ok(employee);
        }
    }
}

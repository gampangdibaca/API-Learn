using API;
using API.ViewModel;
using Client.Base;
using Client.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository): base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<JsonResult> GetRegistered()
        {
            var result = await employeeRepository.GetRegistered();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetEmployee()
        {
            var result = await employeeRepository.GetEmployee();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetGenderDistribution()
        {
            var result = await employeeRepository.GetEmployeeGenderDistribution();
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Register(RegisterVM registerVM)
        {
            var result = await employeeRepository.Register(registerVM);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetEmployeeByNIK(GetByNIKVM getByNIKVM)
        {
            var result = await employeeRepository.GetEmployeeByNIK(getByNIKVM);
            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

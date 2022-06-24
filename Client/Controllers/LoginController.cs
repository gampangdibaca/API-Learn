using API.ViewModel;
using Client.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository loginRepository;
        public LoginController(LoginRepository loginRepository)
        {
            this.loginRepository = loginRepository;
        }

        [HttpPost("Auth/")]
        public async Task<JsonResult> Auth(LoginVM loginVM)
        {
            var jwtToken = await loginRepository.Auth(loginVM);
            var token = jwtToken.idToken;
            var name = jwtToken.Name;
            if (token != null)
            {
                HttpContext.Session.SetString("JWToken", token);
                HttpContext.Session.SetString("Name", name);
                HttpContext.Session.SetString("ProfilePicture", "/resources/img/default-user.png");
            }
            return Json(jwtToken);
        }
        [Authorize]
        [HttpGet("Logout/")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("JWToken") != null)
            {
                return RedirectToAction("index", "Dashboard");
            }
            return View();
        }
    }
}

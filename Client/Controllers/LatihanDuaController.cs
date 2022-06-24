using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager, Director")]
    public class LatihanDuaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

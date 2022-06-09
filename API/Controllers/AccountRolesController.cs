using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("signManager")]
        public ActionResult SignManager(SignManagerVM signManagerVM)
        {
            var result = accountRoleRepository.SignManager(signManagerVM.NIK);
            switch (result)
            {
                case 0:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Gagal Sign Manager" });
                case -1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, 
                        message = $"Employee dengan NIK: {signManagerVM.NIK} sudah memiliki role Manager!!!" });
                default:
                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Berhasil Sign Manager!!!" });
            }
        }
    }
}

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
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;

        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            string idToken = "";
            string name = "";
            var result = accountRepository.VerifyLogin(loginVM, _configuration, out idToken, out name);
            switch (result)
            {
                case -1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar!!!" });
                case -2:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Error Account!!!" });
                case -3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Invalid Password!!!" });
                default:
                    return StatusCode(200, new { status = HttpStatusCode.OK, idToken, message = "Login Berhasil!!!", Name = name });
            }
        }

        [HttpPost("forgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var result = accountRepository.ForgotPassword(forgotPasswordVM.Email);
            switch (result)
            {
                case -1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar!!!" });
                case -2:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Error Account!!!" });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "OTP sudah terkirim ke Email anda, silahkan cek Email anda" });
        }

        [HttpPost("changePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);
            switch (result)
            {
                case -1:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak terdaftar!!!" });
                case -2:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Error Account!!!" });
                case -3:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP Salah!!!" });
                case -4:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP sudah kadaluarsa!!!" });
                case -5:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP sudah digunakan!!!" });
                case -6:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "New Password dan Confirm Password berbeda!!!" });
                case -7:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = " New Password tidak boleh sama dengan Old Password!!!" });
                case -8:
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Gagal Ganti Password!!!" });
                default:
                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Berhasil Ganti Password!!!" });
            }
        }

        [Authorize]
        [HttpGet("jwt")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }
    }


}

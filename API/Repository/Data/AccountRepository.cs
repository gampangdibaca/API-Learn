using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {

            
        }

        public int VerifyLogin(LoginVM loginVM, IConfiguration _configuration, out string idToken)
        {
            const int InvalidEmail = -1;
            const int InvalidAccount = -2;
            const int InvalidPassword = -3;
            const int Success = 200;
            idToken = "";
            Employee employee = (from e in myContext.Employees where e.Email == loginVM.Email select e).FirstOrDefault();
            if (employee == null)
            {
                return InvalidEmail;
            }
            Account account = myContext.Accounts.Find(employee.NIK);
            if (account == null)
            {
                return InvalidAccount;
            }
            if (!(BC.BCrypt.Verify(loginVM.Password, account.Password)))
            {
                return InvalidPassword;
            }

            List<Claim> claims = new List<Claim>();
            var accountRoles = (from ar in myContext.AccountRole 
                                join r in myContext.Role on ar.Roles_Id equals r.Id 
                                where ar.NIK == employee.NIK select r.Name);
            claims.Add(new Claim("Email", employee.Email));
            foreach (var accountRole in accountRoles)
            {
                claims.Add(new Claim("roles", accountRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );
            idToken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idToken.ToString()));
            
            return Success;
        }

        public int ForgotPassword(string email)
        {
            const int InvalidEmail = -1;
            const int InvalidAccount = -2;
            const int FailedUpdateAccount = -3;

            Employee employee = (from e in myContext.Employees where e.Email == email select e).FirstOrDefault();
            if (employee == null)
            {
                return InvalidEmail;
            }
            bool isUniqueOTP = false;
            string OTP = "";
            while (!isUniqueOTP)
            {
                OTP = GenerateOTP();
                Account account = (from a in myContext.Accounts where (a.OTP == OTP) && (a.IsActiveOTP == true) select a).FirstOrDefault();
                if (account == null)
                {
                    isUniqueOTP = true;
                }
            }

            Account ForgotAccount = myContext.Accounts.Find(employee.NIK);
            if (ForgotAccount == null)
            {
                return InvalidAccount;
            }

            ForgotAccount.ExpiredTime = DateTime.Now.AddMinutes(5);
            ForgotAccount.OTP = OTP;
            ForgotAccount.IsActiveOTP = true;
            var result = Update(ForgotAccount, employee.NIK);
            if (result <= 0)
            {
                return FailedUpdateAccount;
            }

            var client = new SmtpClient("smtp.ethereal.email", 587)
            {
                Credentials = new NetworkCredential("magali.mann@ethereal.email", "v41SCQcRkCuQg4A19v"),
                EnableSsl = true
            };
            client.Send("magali.mann@ethereal.email", employee.Email, "Forgot Password", $"Your OTP is: {OTP}");
            return 200;
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            const int InvalidEmail = -1;
            const int InvalidAccount = -2;
            const int InvalidOTP = -3;
            const int ExpiredOTP = -4;
            const int UsedOTP = -5;
            const int DifferentConfirm = -6;
            const int SamePassword = -7;
            const int FailedChangePassword = -8;
            
            Employee employee = (from e in myContext.Employees where e.Email == changePasswordVM.Email select e).SingleOrDefault();
            if (employee == null)
            {
                return InvalidEmail;
            }
            Account account = myContext.Accounts.Find(employee.NIK);
            if (account == null)
            {
                return InvalidAccount;
            }
            if (changePasswordVM.OTP != account.OTP)
            {
                return InvalidOTP;
            }

            if (DateTime.Now > account.ExpiredTime)
            {
                return ExpiredOTP;
            }

            if(account.IsActiveOTP == false)
            {
                return UsedOTP;
            }

            if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
            {
                return DifferentConfirm;
            }

            if (BC.BCrypt.Verify(changePasswordVM.NewPassword, account.Password))
            {
                return SamePassword;
            }

            account.IsActiveOTP = false;
            account.Password = BC.BCrypt.HashPassword(changePasswordVM.NewPassword);
            var result =  Update(account, employee.NIK);
            if (result <= 0)
            {
                return FailedChangePassword;
            }
            return 200;
        }

        private string GenerateOTP()
        {
            StringBuilder OTP = new StringBuilder();
            while (OTP.Length < 6)
            {
                OTP.Append(new Random().Next(0, 9));
            }
            return OTP.ToString();
        }
    }
}

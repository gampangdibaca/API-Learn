using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        public DateTime ExpiredTime { get; set; }
        public bool IsActiveOTP { get; set; }
        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}

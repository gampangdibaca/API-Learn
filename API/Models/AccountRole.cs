using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        public string NIK { get; set; }
        public int Roles_Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}

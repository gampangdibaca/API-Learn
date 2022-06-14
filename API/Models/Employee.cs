using API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        //[Required(ErrorMessage = "Email Harus Diisi!!!")]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public virtual Account Account { get; set; }
        public bool IsDeleted { get; set; }

        public Employee()
        {

        }

    }

    public enum Gender
    {
        Male,
        Female
    }
}

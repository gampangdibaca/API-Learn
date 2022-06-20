using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class GetByNIKResponseVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int[] Roles { get; set; }
        public int EducationId { get; set; }
        public string GPA { get; set; }
        public int UniversityId { get; set; }
        public string Degree { get; set; }
    }
}

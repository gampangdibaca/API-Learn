using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
        }

        public int Register(RegisterVM registerVM)
        {
            const int DuplicatePhone = -1;
            const int DuplicateEmail = -2;
            const int UniversityNotFound = -3;
            const int InvalidDegree = -4;
            const int InvalidLastNIK = -5;
            const int InvalidGender = -6;

            Employee employeeCheck = myContext.Employees
                .Where(e => e.Phone == registerVM.Phone)
                .FirstOrDefault();
            if(employeeCheck != null)
            {
                return DuplicatePhone;
            }
            employeeCheck = myContext.Employees
                .Where(e => e.Email == registerVM.Email)
                .FirstOrDefault();
            if (employeeCheck != null)
            {
                return DuplicateEmail;
            }
            Education education = new Education();
            University university = myContext.Universities.Find(registerVM.UniversityId);
            if(university == null)
            {
                return UniversityNotFound;
            }
            education.University = university;
            education.GPA = registerVM.GPA;
            Degree ParsedDegree;
            bool isValidDegree = Enum.TryParse(registerVM.Degree, out ParsedDegree);
            if (!isValidDegree)
            {
                return InvalidDegree;
            }
            education.Degree = ParsedDegree;

            Profiling profiling = new Profiling();
            profiling.Education = education;

            Account account = new Account();
            account.Password = BC.BCrypt.HashPassword(registerVM.Password);
            account.Profiling = profiling;

            Employee employee = new Employee();
            string lastNIK = (from e in myContext.Employees orderby e.NIK select e.NIK).LastOrDefault();

            if (!String.IsNullOrWhiteSpace(lastNIK))
            {
                if (!(lastNIK.Length > 8))
                {
                    return InvalidLastNIK;
                }
                int LastDigit;

                bool isValidDigit = int.TryParse(lastNIK[8..], out LastDigit);
                if (!isValidDigit)
                {
                    return InvalidLastNIK;
                }
                lastNIK = (LastDigit+1) + "";
                while (lastNIK.Length!=4)
                {
                    lastNIK = "0" + lastNIK;
                }
            } else
            {
                lastNIK = "0001";
            }

            
            string currDate = DateTime.Now.ToString("MM/dd/yyyy");
            currDate = currDate.Replace("/", "");

            employee.NIK = currDate+lastNIK;
            employee.FirstName = registerVM.FirstName;
            employee.LastName = registerVM.LastName;
            employee.Phone = registerVM.Phone;
            employee.BirthDate = registerVM.BirthDate;
            employee.Salary = registerVM.Salary;
            employee.Email = registerVM.Email;
            employee.Account = account;
            Gender ParsedGender;
            bool isValidGender = Enum.TryParse(registerVM.Gender, out ParsedGender);
            if (!isValidGender)
            {
                return InvalidGender;
            }
            employee.Gender = ParsedGender;
            employee.IsDeleted = false;
            //var result = Insert(employee);
            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            AccountRole accountRole = new AccountRole
            {
                NIK = employee.NIK,
                Roles_Id = 1
            };
            myContext.AccountRole.Add(accountRole);
            myContext.SaveChanges();
            return result;
        }

        public List<RegisteredDataVM> GetRegisteredEmployeeData()
        {
            List<RegisteredDataVM> registeredDatas = new List<RegisteredDataVM>();

            foreach (Employee employee in myContext.Employees.ToList())
            {
                RegisteredDataVM registeredData = new RegisteredDataVM();
                registeredData.FullName = $"{employee.FirstName} {employee.LastName}";
                registeredData.Phone = employee.Phone;
                registeredData.BirthDate = employee.BirthDate;
                registeredData.Salary = employee.Salary;
                registeredData.Gender = employee.Gender.ToString();
                registeredData.Email = employee.Email;
                Profiling profiling = myContext.Profilings.Find(employee.NIK);
                Education education = myContext.Educations.Find(profiling.Education_Id);
                registeredData.Degree = education.Degree.ToString();
                registeredData.GPA = education.GPA;
                registeredData.UniversityName = myContext.Universities.Find(education.University_Id).Name;
                registeredDatas.Add(registeredData);
            }

            return registeredDatas;
        }
        
    }
}

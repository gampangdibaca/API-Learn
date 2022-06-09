using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            Employee employee = context.Employees.Find(NIK);
            if (employee != null)
            {
                // detach
                context.Entry(employee).State = EntityState.Detached;
            }

            return employee;
        }

        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }

        public Employee FirstGender(string gender)
        {
            return context.Employees
                .FirstOrDefault(emp => emp.Gender == Enum.Parse<Gender>(gender));
        }

        public Employee SingleEmail(string email)
        {
            return context.Employees
                .SingleOrDefault(emp => emp.Email == email);
        }
    }
}

using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {

        }

        public List<University> GetUniversities()
        {
            List<University> lu = new List<University>();
            foreach (University university in myContext.Universities)
            {
                lu.Add(new University { Id = university.Id, Name=university.Name }) ;
            }
            return lu;
        }
    }
}

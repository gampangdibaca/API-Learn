using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, int>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {

        }

        public List<SelectByUniversityVM> GetUniversityDistribution()
        {
            //List<SelectByUniversityVM> result = new List<SelectByUniversityVM>();
            //List <University> universities = myContext.Universities.ToList();
            //foreach (University university in universities)
            //{
            //    result.Add(new SelectByUniversityVM
            //    {
            //        Name = university.Name,
            //        Count = (from e in myContext.Educations where e.University_Id == university.Id select e).Count()
            //    });
            //}

            List<SelectByUniversityVM> result = (from e in myContext.Educations
                     join u in myContext.Universities
                     on e.University_Id equals u.Id
                     group e by u.Name into g
                     select new SelectByUniversityVM
                     {
                         Name = g.Key,
                         Count = g.Count()
                     }).ToList();
            return result;
        }
    }
}

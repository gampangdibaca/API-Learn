using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {

        }

        public int SignManager(string NIK)
        {
            const int AlreadyManager  = -1;
            AccountRole isManager = myContext.AccountRole.Where(ar => ar.NIK == NIK && ar.Roles_Id == 2).SingleOrDefault();
            if(isManager != null)
            {
                return AlreadyManager;
            }

            AccountRole accountRole = new AccountRole()
            {
                NIK = NIK,
                Roles_Id = 2
            };
            myContext.AccountRole.Add(accountRole);
            var result = myContext.SaveChanges();

            return result;
        }
    }
}

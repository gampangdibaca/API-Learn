using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel.ResponseViewModels
{
    public class GetEmployeeVM : BaseResponseVM
    {
        public List<Employee> data { get; set; }
    }
}

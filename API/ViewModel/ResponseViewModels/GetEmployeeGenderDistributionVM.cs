using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel.ResponseViewModels
{
    public class GetEmployeeGenderDistributionVM : BaseResponseVM
    {
        public Dictionary<string, int> data { get; set; }
    }
}

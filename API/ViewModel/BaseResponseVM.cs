using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class BaseResponseVM
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}

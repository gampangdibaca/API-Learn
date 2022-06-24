using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class JWTokenVM
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string idToken { get; set; }
        public string Name { get; set; }
    }
}

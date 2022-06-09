using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }

        public int Education_Id { get; set; }

        public Account Account { get; set; }

        public Education Education { get; set; }
    }
}

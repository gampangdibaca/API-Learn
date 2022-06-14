using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Degree Degree { get; set; }
        public string GPA { get; set; }
        public int University_Id { get; set; }
        public virtual ICollection<Profiling> Profilings { get; set; }
        public virtual University University { get; set; }
    }

    public enum Degree
    {
        D3,
        D4,
        S1,
        S2,
        S3
    }
}

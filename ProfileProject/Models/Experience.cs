using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Experience
    {
        public int Id {get; set;}
        public int UserId {get; set;}

        public string MyExperience  {get; set;}  = default!; 
        public string Description {get; set;} = default!;
    }
}
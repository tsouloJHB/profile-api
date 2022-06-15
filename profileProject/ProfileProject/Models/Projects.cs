using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Projects
    {
        public int Id {get; set;}
        public int UserId {get; set;}

        public string MyProjects  {get; set;} = default!;
        public string Description {get; set;} = default!;
    }
}
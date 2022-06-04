using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Skills
    {
       public int Id {get; set;}
        public int UserId {get; set;}

        public string MySkills  {get; set;} = default!;
    }
}
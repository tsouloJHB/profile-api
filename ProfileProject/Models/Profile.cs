using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Profile
    {
        public int UserId {get;set;}
        public string about  {get; set;} = default!;
        public string MyEducation  {get; set;} = default!;
        public string MyEducationDescription  {get; set;} = default!;
        public string MyExperience  {get; set;}  = default!; 
         public string MyExperienceDescription  {get; set;}  = default!;
        public string MyProjects  {get; set;} = default!;
        public string MyProjectsDescription  {get; set;} = default!;
        public string MySkills  {get; set;} = default!;
        public string name {get;set;}  = default!;
        public string surname {get;set;}  = default!;
        public string email {get;set;}  = default!;
        public int cell {get;set;}
        public string currentOccupation {get;set;}  = default!;
    }
    
}
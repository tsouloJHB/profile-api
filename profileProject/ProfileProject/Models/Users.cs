using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Users
    {
        public int id {get;set;}
        public string name {get;set;}  = default!;
        public string surname {get;set;}  = default!;
        public string email {get;set;}  = default!;
        public int cell {get;set;}
        public string currentOccupation {get;set;}  = default!;
        public string image {get;set;} = default!;
    }
}
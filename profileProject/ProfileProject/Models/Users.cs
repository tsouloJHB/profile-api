using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileProject.Models
{
    public class Users
    {
        [Key]
        public int Id {get;set;}
        public string name {get;set;}  = default!;
        public string surname {get;set;}  = default!;
        public string email {get;set;}  = default!;
        public int cell {get;set;}
        public string currentOccupation {get;set;}  = default!;
        public string image {get;set;} = default!;
        public string Theme { get; set; } = default!;
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
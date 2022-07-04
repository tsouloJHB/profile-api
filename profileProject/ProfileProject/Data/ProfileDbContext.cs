using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfileProject.Data
{
    public class ProfileDbContext : DbContext , IProfileDbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options){

        }
        public DbSet<About> Abouts {get; set;}
        public DbSet<Users> users {get; set;}
        public DbSet<Education> Educations {get; set;}
        public DbSet<Experience> Experiences {get; set;}
        public DbSet<Projects> Projects {get; set;}
        public DbSet<Skills> Skills {get; set;}
    }
}
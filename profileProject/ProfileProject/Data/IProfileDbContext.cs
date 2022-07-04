using Microsoft.EntityFrameworkCore;
using ProfileProject.Models;

namespace ProfileProject.Data;

public interface IProfileDbContext
{
    public DbSet<About> Abouts {get; set;}
    public DbSet<Users> users {get; set;}
    public DbSet<Education> Educations {get; set;}
    public DbSet<Experience> Experiences {get; set;}
    public DbSet<Projects> Projects {get; set;}
    public DbSet<Skills> Skills {get; set;}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfileProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController :  ControllerBase
    {
               private readonly ProfileDbContext _context;  
               public ProfileController(ProfileDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Profile>> GetProfile(){
            var list1 = await _context.Educations.ToListAsync();
            List<Users> value = await _context.users.ToListAsync();
            List<Education> educations = await _context.Educations.ToListAsync();
            List<About> about = await _context.Abouts.ToListAsync();
             List<Profile> li = new  List<Profile>();
            foreach(var item in value ){
                //Console.WriteLine(item.name);

                Profile profiler = new Profile();
                //Console.WriteLine(item.name);
                profiler.name = item.name;
                profiler.surname = item.surname;

                foreach(var edu in educations){
                    if(edu.UserId == item.id){
                        profiler.MyEducation = edu.MyEducation;
                    }
                }
                foreach(var edu in about){
                    if(edu.UserId == item.id){
                        profiler.MyEducation = edu.about;
                    }
                }
                li.Add(profiler);
            //  var result = (from a in _context.users
            //     select new {
            //         name = a.name,
            //         surname = a.surname
            //     } );
            // foreach(var ls in result){
            //     Console.WriteLine(ls);
            // }    
             //li.Add(result);   
            }

            foreach(var lit in li){
                
                Console.WriteLine(lit.name);
            }
            

            //  var  result =  (from a in _context.users
         
            //     select  new {
            //         about = a.name,
            //         id = a.surname,
         
                  
            //     } );
                


            //  List<Profile> profileLists = new List<Profile>();
            //   Profile profile = new Profile();   
            //     foreach(var item in result){
            //         profile.about = item.about;
            //         profileLists.Add(profile);
            //     }
                
            var  results = (from a in _context.Abouts
                join b in _context.Educations on a.UserId-1 equals b.UserId 
                join c in _context.Experiences on a.UserId-1 equals c.UserId
                join d in _context.Projects on a.UserId-1 equals d.UserId
                join e in _context.users on a.UserId-1 equals e.id
                join f in _context.Skills on a.UserId-1 equals f.UserId
                select new {
                    about = a.about,
                    education = b.MyEducation,
                    experience = c.MyExperience,
                    projects = d.MyProjects,
                    cell = e.cell,
                    name = e.name,
                    surname = e.surname,
                    email = e.email,
                    occupation = e.currentOccupation,
                    id = e.id 
                } );
            Profile profile = new Profile();
            List<Profile> profileList = new List<Profile>();   
                foreach(var item in results){
                    profile.about = item.about;
                    profile.MyEducation = item.education;
                    profile.MyExperience = item.experience;
                    profile.MyProjects = item.projects;
                    profile.cell = item.cell;
                    profile.email = item.email;
                    profile.currentOccupation = item.occupation;
                    profile.name = item.name;
                    profile.surname = item.surname;
                    profile.UserId = item.id;
                    profileList.Add(profile);
            }    
            return li; 

         }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Users), StatusCodes.Status404NotFound)]
         public async Task<IActionResult> GetById(string name){
             var users = await _context.users.FindAsync(name);
             return users == null ? NotFound() : Ok(users);
         }
        // [HttpGet("name")]
        // [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(About), StatusCodes.Status404NotFound)]   
        // public async Task<IActionResult> GetUser(string name){
        //     var users = await _context.users.FindAsync();
        //     return users == null ? NotFound() : Ok(users);
        //  }

        [HttpGet("name")]
        [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(About), StatusCodes.Status404NotFound)]
         public async Task<IEnumerable<Profile>> GetByName(string name){
             var list1 = await _context.Educations.ToListAsync();
               var  results = (from a in _context.users
                where a.name == name
           
                select new {
                    id = a.id,
                    name = a.name,
                    surname = a.surname,
             
                } );
            Users users =  new Users();
            foreach(var item in results){
                users.id = item.id;
            }
            var  result = (from a in _context.Abouts where a.Id == users.id
                join b in _context.Educations on users.id equals b.UserId 
                join c in _context.Experiences on users.id  equals c.UserId
                join d in _context.Projects on users.id  equals d.UserId
                join e in _context.users on users.id  equals e.id
                join f in _context.Skills on users.id  equals f.UserId
                select new {
                    about = a.about,
                    education = b.MyEducation,
                    experience = c.MyExperience,
                    projects = d.MyProjects,
                    cell = e.cell,
                    name = e.name,
                    surname = e.surname,
                    email = e.email,
                    occupation = e.currentOccupation,
                    id = e.id 
                } );
            Profile profile = new Profile();
            List<Profile> profileList = new List<Profile>();   
                foreach(var item in result){
                    profile.about = item.about;
                    profile.MyEducation = item.education;
                    profile.MyExperience = item.experience;
                    profile.MyProjects = item.projects;
                    profile.cell = item.cell;
                    profile.email = item.email;
                    profile.currentOccupation = item.occupation;
                    profile.name = item.name;
                    profile.surname = item.surname;
                    profile.UserId = item.id;
                    profileList.Add(profile);
            }    
            
            
             
            return profileList;
         }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProfile(Profile profile){

            Users users = new Users();
            users.name = profile.name;
            users.surname = profile.surname;
            users.cell = profile.cell;
            users.currentOccupation = profile.currentOccupation;
            users.email = profile.email;
            await _context.users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            


            Education education = new Education();
           
            education.UserId = users.id;
            education.MyEducation = profile.MyEducation;
            await _context.Educations.AddAsync(education);

            About about = new About();
            about.UserId = users.id;
            about.about = profile.about;
            await _context.Abouts.AddRangeAsync(about);

            Experience experience = new Experience();
            experience.UserId = users.id;
            experience.MyExperience = profile.MyExperience;
            await _context.Experiences.AddRangeAsync(experience);

            Projects projects = new Projects();
            projects.UserId = users.id;
            projects.MyProjects = profile.MyProjects;
            await _context.Projects.AddRangeAsync(projects);

            Skills skills = new Skills();
            skills.UserId = users.id;
            skills.MySkills = profile.MySkills;
            await _context.Skills.AddRangeAsync(skills);


            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), profile);
        }   

    }
}
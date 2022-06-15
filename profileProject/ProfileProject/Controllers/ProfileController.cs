using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using Microsoft.EntityFrameworkCore;
using ProfileProject.Services;

namespace ProfileProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController :  ControllerBase
    {
        private readonly ProfileDbContext _context;  
        private readonly IProfile  _profileService;
        public ProfileController(ProfileDbContext context, IProfile profileService)
        {
            this._context = context;
            this._profileService = profileService;
        }
        

        [HttpGet]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status404NotFound)]
        public IEnumerable<Profile> GetProfile(){
           
            return _profileService.GetAllProfiles().Result; 

         }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Users), StatusCodes.Status404NotFound)]
         public async Task<IActionResult> GetById(string id){
             var users = await _context.users.FindAsync(id);
             return users == null ? NotFound() : Ok(users);
         }

        [HttpGet("name")]
        [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(About), StatusCodes.Status404NotFound)]
         public IEnumerable<Profile> GetByName(string name){

             return _profileService.GetProfileByName(name).Result;
         }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateProfile(Profile profile)
        {

            _profileService.CreateProfile(profile);
            return CreatedAtAction(nameof(GetById), profile);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<IActionResult> Update(int id,Profile profile)
        {
            var list1 = await _context.Educations.ToListAsync();
            //check if url and body id match
            if(id != profile.UserId) return BadRequest();
            //create user

            _profileService.UpdateProfile(profile);
            // await _context.SaveChangesAsync();
            return NoContent();
        }
        
        // none api methods

        private  int GetTableByUserId(int userId,  List<Experience> list1)
        {
            //var list1 = await _context.Educations.ToListAsync();
            var  results = (from a in list1
                where a.UserId == userId
            
                select new {
                    id = a.Id,
            
                } );
            return results.First().id;
        }
        private int GetTableByUserId(int userId,  List<Education> list1)
        {
            var  results = (from a in list1
                where a.UserId == userId
            
                select new {
                    id = a.Id,
            
                } );
            return results.First().id;
        }
        private int GetTableByUserId(int userId,  List<Projects> list1)
        {
            var  results = (from a in list1
                where a.UserId == userId
            
                select new {
                    id = a.Id,
            
                } );
            return results.First().id;
        }
        private int GetTableByUserId(int userId,  List<About> list1)
        {
            var  results = (from a in list1
                where a.UserId == userId
            
                select new {
                    id = a.Id,
            
                } );
            return results.First().id;
        }

        
        private int GetTableByUserId(int userId,  List<Skills> list1)
        {
            var  results = (from a in list1
                where a.UserId == userId
            
                select new {
                    id = a.Id,
            
                } );
            return results.First().id;
        }
        
      
    }
    }
    
  
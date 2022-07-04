using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using Microsoft.EntityFrameworkCore;
using ProfileProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ProfileProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController :  ControllerBase
    {
        // private readonly ProfileDbContext _context;
        private readonly IProfileDbContext _context;
        private readonly IProfile  _profileService;
        
        public ProfileController(IProfileDbContext context, IProfile profileService)
        {
            this._context = context;
            this._profileService = profileService;
        }
        

        [HttpGet,Authorize]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status404NotFound)]
        public IEnumerable<Profile> GetProfile(){
           
            return _profileService.GetAllProfiles().Result; 

         }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Users), StatusCodes.Status404NotFound)]
         public async Task<IActionResult> GetById(int id){
             var users = await _context.users.FindAsync(id);
             return users == null ? NotFound() : Ok(users);
         }

        [HttpGet("name")]
        [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(About), StatusCodes.Status404NotFound)]
         public  IEnumerable<Profile> GetByName(string name)
         {
             List<Profile> profile = new List<Profile>();
             profile.Add(_profileService.GetProfileByName(name).Result);
             return profile;
         }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateProfile(Profile profile)
        {
            await _profileService.CreateProfile(profile);
            return CreatedAtAction(nameof(GetById), profile);
        }
        [EnableCors("CorsApi")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<IActionResult> Update(int id,Profile profile)
        {
            var list1 = await _context.Educations.ToListAsync();
            //check if url and body id match
            if (id != profile.UserId) return BadRequest();

            await _profileService.UpdateProfile(profile);
            // await _context.SaveChangesAsync();
            return NoContent();
        }
    }
    }
    
  
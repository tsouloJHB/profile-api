using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using Microsoft.EntityFrameworkCore;
using ProfileProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace ProfileProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        // private readonly ProfileDbContext _context;
        private readonly ProfileDbContext _context;
        private readonly IProfile _profileService;

        public ProfileController(ProfileDbContext context, IProfile profileService)
        {
            this._context = context;
            this._profileService = profileService;
        }


        [HttpGet, Authorize]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status404NotFound)]
        public IEnumerable<Profile> GetProfile()
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Console.WriteLine(rawId);
            if (rawId.Equals(""))
            {
                //return Unauthorized();
            }

            int id = Int16.Parse(rawId);
            List<Profile> profile = new List<Profile>();
            profile.Add(_profileService.GetProfileById(id).Result);
            return profile;
            

        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Users), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var users = await _context.users.FindAsync(id);
            return users == null ? NotFound() : Ok(users);
        }

        [HttpGet("name")]
        [ProducesResponseType(typeof(About), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(About), StatusCodes.Status404NotFound)]
        public IEnumerable<Profile> GetByName(string name)
        {
            List<Profile> profile = new List<Profile>();
            profile.Add(_profileService.GetProfileByName(name).Result);
            return profile;
        }

        [HttpPost("imageupload"), Authorize, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage()
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Console.WriteLine(rawId);
            if (rawId.Equals(""))
            {
                return Unauthorized();
            }

            int id = Int16.Parse(rawId);
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    Console.WriteLine(file.ContentType);

                    //var fileNamer = ContentDispositionHeaderValue.Parse(file.ContentDisposition).DispositionType;
                    //string time = DateTime.Now.ToString("h:mm:ss tt");
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                    //string newFileName = fileName.ToString().Split('.')[0]+time+ContentDispositionHeaderValue.Parse(file.ContentDisposition).;
                    var fullPath = Path.Combine(pathToSave, fileName.ToString());
                    var dbPath = Path.Combine(folderName, fileName.ToString());
                    await using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    //save to angular project for presentation sake
                    var angularPath = Path.Combine(@"C:\Users\soulo\Documents\project\soulo\src\assets\images\",
                        fileName.ToString());

                    await using (var stream = new FileStream(angularPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Console.WriteLine(dbPath);
                    //save file to database
                    var users = await _context.users.FindAsync(id);
                    var image = Path.Combine(@"../../assets/images/", fileName.ToString());
                    users.image = image;
                    _context.Entry(users).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    Console.WriteLine(users.image);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, $"Internal server error: {ex}");
            }

            return NoContent();
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
        public async Task<IActionResult> Update(int id, Profile profile)
        {
            var list1 = await _context.Educations.ToListAsync();
            //check if url and body id match
            if (id != profile.UserId) return BadRequest();
            var user = await _context.users.FindAsync(id);
            await _profileService.UpdateProfile(profile,user);
            // await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [EnableCors("CorsApi")]
        [HttpPut("UpdateProfile"),Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile(Profile profile)
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Console.WriteLine(rawId);
            if (rawId.Equals(""))
            {
                //return Unauthorized();
            }
            int id = Int16.Parse(rawId);
            profile.UserId = id;
            var list1 = await _context.Educations.ToListAsync();
            //check if url and body id match
            if (id != profile.UserId) return BadRequest();
            var user = await _context.users.FindAsync(id);
            await _profileService.UpdateProfile(profile,user);
           
            return NoContent();
        }

        [HttpPut("EditTheme"), Authorize]
        public async Task<ThemeEdit> ThemeEdit(ThemeEdit themeEdit)
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Console.WriteLine(rawId);
            if (rawId.Equals(""))
            {
                //return Unauthorized();
            }

            int id = Int16.Parse(rawId);

            //get theme and modify
            ThemeEdit foundThemeEdit = await _profileService.UpdateTheme(themeEdit.JsonCode, themeEdit.Theme, id);
            return foundThemeEdit;
        }

        [HttpGet("GetTheme"), Authorize]
        public async Task<ThemeEdit> GetTheme()
        {
            string rawId = HttpContext.User.FindFirstValue("id");
            Console.WriteLine(rawId);
            if (rawId.Equals(""))
            {
                //return Unauthorized();
            }

            int id = Int16.Parse(rawId);
            ThemeEdit theme = await _profileService.GetTheme(id);
            return theme;
        }

        [HttpGet("GetThemeByName")]
        public async Task<ThemeEdit> GetThemeByName(string name)
        {
             return await _profileService.GetThemeByName(name);
        }
    }
}
    
      
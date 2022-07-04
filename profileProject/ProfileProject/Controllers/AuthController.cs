using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProfileProject.Models;
using ProfileProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;


namespace ProfileProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuth _auth;
    private readonly IProfile _profileService;
    public static User user = new User();

    public AuthController(IConfiguration configuration, IAuth auth, IProfile profile)
    {
        _configuration = configuration;
        _auth = auth;
        _profileService = profile;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
       
        CreatePasswordHash(request.Password,out byte[] passwordHash,out byte[] passwordSalt);
        user.Email = request.Email;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        Profile profile = new Profile()
        {
            email = request.Email,
            name = request.Name,
            surname = request.Surname,
            cell = request.Cell,
            image = "",
            // UserId = "",
            Theme = "classic",
            currentOccupation = "",
            about = "",
            MyEducation  = "",
            MyEducationDescription  = "",
            MyExperience = "",
            MyExperienceDescription = "",
            MyProjects  = "",
            MyProjectsDescription  = "",
            MySkills = "",
            
        };
        await _auth.CreateProfile(profile,passwordHash,passwordSalt);
        return Ok(user);

    }
    [HttpGet,Authorize]
    [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status404NotFound)]
    public IEnumerable<Profile> GetProfile(){
           
        return _profileService.GetAllProfiles().Result; 

    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email)

        };
        //create key getting key from appsettings
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:token").Value));
        //create  credentials
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //create token
        var token = new JwtSecurityToken(
            claims:claims,
            expires:DateTime.Now.AddDays(1),
            signingCredentials:creds
        );

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }


    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginDto request)
    {
     
        Users user1 =  _auth.GetUserByEmail(request.Email.ToLower());
       
        if (user1.email.IsNullOrEmpty()) {
            return BadRequest("User not found.");
        }
        
        Console.WriteLine(user1.PasswordHash+" "+user1.PasswordSalt);
        if (!VerifyPasswordHah(request.Password,user1.PasswordHash,user1.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }
        var newRefreshToken = GenerateRefreshToken();
        
        SetRefreshToken(newRefreshToken,user1);
        string token = CreateToken(user);

        //

        return Ok(token);
    }
    [EnableCors("CorsApi")]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken()
    {
        
        var refreshToken = Request.Cookies["refreshToken"];
        Console.WriteLine(refreshToken);
        Users user1 = _auth.GetUserByRefreshToken(refreshToken).Result;
        Console.WriteLine(user1.name);
        if (!user1.RefreshToken.Equals(refreshToken))
        {
            return Unauthorized("Invalid Refresh Token.");
        }
        else if(user.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired.");
        }

        string token = CreateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        //Users user1 = _auth.GetUserByRefreshToken(newRefreshToken).Result;
        SetRefreshToken(newRefreshToken,user1);

        return Ok(token);
    }

    private void SetRefreshToken(RefreshToken newRefreshToken,Users user1)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        //edit user
        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.Created;
        user.TokenExpires = newRefreshToken.Expires;
      
        _auth.UserUpdateRefreshToken(newRefreshToken,user1);
    }
    
    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow
        };

        return refreshToken;
    }

    private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHah(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
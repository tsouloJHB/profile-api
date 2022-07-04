using Microsoft.EntityFrameworkCore;
using ProfileProject.Data;
using ProfileProject.Models;


namespace ProfileProject.Services;

public class AuthService: IAuth
{
    private readonly ProfileDbContext _context;
    private readonly IProfile _profileService;

    public AuthService(ProfileDbContext context,IProfile profile)
    {
        _context = context;
        _profileService = profile;
    }
    public Users GetUserByEmail(string email)
    {
       // SqlCommand cmd = new SqlCommand ("select * from users");
      // var users = await _context.users.ToListAsync();
       var  results = (from a in _context.users
           where a.email == email
           select new Users
           {
              name = a.name,
              surname = a.surname,
              email = a.email,
              cell = a.cell,
              PasswordHash = a.PasswordHash,
              PasswordSalt = a.PasswordSalt,
              Id = a.Id
           } );
     // check if email is not null than return value
     Users user = new Users();
     if (results.Any()){
         foreach (var use in results){
             user = use;
         }
     }
     return user;
    }
    
    public async Task<Profile> CreateProfile(Profile profile,byte[] passwordHash,byte[] passwordSalt)
    {
        Users users = _profileService.CreateUser(profile);
        users.PasswordHash = passwordHash;
        users.PasswordSalt = passwordSalt;
        await _context.AddAsync(users);
        await _context.SaveChangesAsync();
        profile.UserId = users.Id;
        await _context.Educations.AddAsync( _profileService.CreateEducation(profile));
        await _context.Abouts.AddAsync( _profileService.CreateAbout(profile));
        await _context.Experiences.AddAsync( _profileService.CreateExperience(profile));
        await _context.Skills.AddAsync( _profileService.CreateSkills(profile));
        await _context.Projects.AddAsync( _profileService.CreateProject(profile));
        await _context.SaveChangesAsync();
        return profile;
    }

    public async  void UserUpdateRefreshToken(RefreshToken refreshToken,Users user)
    {
        //var user =  _context.users.FindAsync(3);
        var foundUser = await _context.users.FindAsync(user.Id);
        //var user = _profileService.GetUserIdByName(users.name);
        
        Console.WriteLine("Trying to update");
        Console.WriteLine(foundUser.name);
        Console.WriteLine(refreshToken.Token);
        foundUser.TokenCreated = refreshToken.Created;
        foundUser.TokenExpires = refreshToken.Expires;
        foundUser.RefreshToken = refreshToken.Token;
        _context.Entry(foundUser).State = EntityState.Modified;
        var isCompletedSuccessfully = _context.SaveChangesAsync().IsCompletedSuccessfully;
        // return isCompletedSuccessfully;
    }

    public async Task<Users> GetUserByRefreshToken(string refreshToken)
    {
        var userList = await _context.users.ToListAsync();
        // Parallel.ForEach(userList, user =>
        // {
        //     Console.WriteLine(user);     
        //
        // });
        foreach (var user in userList)
        {
            Console.WriteLine(user.RefreshToken);
            if(user.RefreshToken.Equals(refreshToken) )
            {
                return user;
            }
        }

        Users users = new Users();
        return users;
    }

}
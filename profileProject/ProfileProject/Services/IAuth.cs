using ProfileProject.Models;

namespace ProfileProject.Services;

public interface IAuth
{
    public Users GetUserByEmail(string email);
    public  Task<Profile> CreateProfile(Profile profile, byte[] passwordHash, byte[] passwordSalt);
    public void UserUpdateRefreshToken(RefreshToken refreshToken,Users user1);
    public Task<Users> GetUserByRefreshToken(string refreshToken);
}
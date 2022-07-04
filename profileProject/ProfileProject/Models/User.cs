namespace ProfileProject.Models;

public class User
{
    
    public string Email { get; set; } = string.Empty;
    public int UsersId {get; set;}
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }
}
namespace ProfileProject.Models;

public class UserDto
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public int Cell { get; set; } = 1;
    public string Password { get; set; } =  string.Empty;
}
namespace ProfileProject.Models;

public class CommentDto
{
    public DateTime CommentTime { get; set; }
    public string Comment { get; set; }
    public string Image { get; set; }
    public bool Delete { get; set; }
    public string name {get;set;}  = default!;
    public string surname {get;set;}  = default!;
    public string email {get;set;}  = default!;
    public string ProfileImage {get;set;} = default!;
}
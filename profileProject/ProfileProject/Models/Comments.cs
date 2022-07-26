namespace ProfileProject.Models;

public class Comments
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public DateTime CommentTime { get; set; }
    public int PostId { get; set; }
    public int CommentId { get; set; }
    public string Comment { get; set; }
    public string Image { get; set; }
    public bool Delete { get; set; }
}
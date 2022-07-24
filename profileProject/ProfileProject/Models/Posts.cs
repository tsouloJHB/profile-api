namespace ProfileProject.Models;

public class Posts
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public DateTime PostTime { get; set; }
    public string Post { get; set; }
    public string Image { get; set; }
    public bool Delete { get; set; }
}
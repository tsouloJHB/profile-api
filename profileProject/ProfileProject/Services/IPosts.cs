using ProfileProject.Models;

namespace ProfileProject.Services;

public interface IPosts
{
    public Task<List<Posts>> GetAllPost();
    public Task<Posts> CreatePost(Posts posts);
}
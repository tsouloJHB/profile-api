using Microsoft.EntityFrameworkCore;
using ProfileProject.Data;
using ProfileProject.Models;

namespace ProfileProject.Services;

public class PostsService : IPosts
{
    private readonly ProfileDbContext _context;

    public PostsService(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<List<Posts>> GetAllPost()
    {
        List<Posts> value = await _context.Posts.ToListAsync();
        return value;

    }

    public async Task<Posts> CreatePost(Posts posts)
    {
        Posts post = posts;
        await _context.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }
}
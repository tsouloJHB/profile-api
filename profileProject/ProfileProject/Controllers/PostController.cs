using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using ProfileProject.Services;
using Posts = ProfileProject.Models.Posts;

namespace ProfileProject.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly ProfileDbContext _context;
    private readonly IProfile _profileService;
    private readonly IConfiguration _configuration;
    private IPosts _posts;

    public PostController(IConfiguration configuration, IProfile profile, ProfileDbContext context,IPosts posts)
    {
        _configuration = configuration;
        _profileService = profile;
        _context = context;
        _posts = posts;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Posts>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Posts>), StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<PostGetDto>> GetPosts()
    {
        List<Posts> post = _posts.GetAllPost().Result;
        List<PostGetDto> postGetDtos = new List<PostGetDto>();
        foreach (var item in post)
        {
            var user = await _context.users.FindAsync(item.UserId);
            if (user != null)
            {
                PostGetDto postGetDto = new PostGetDto()
                {
                    name = user.name,
                    surname = user.surname,
                    email = user.email,
                    ProfileImage = user.image,
                    Post = item.Post,
                    PostTime = item.PostTime,
                    Image = user.image,
                };
                postGetDtos.Add(postGetDto);
            }

          
        }
        return postGetDtos;
    }
    
    [HttpPost,Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePost(PostDto postDto)
    {
        string rawId = HttpContext.User.FindFirstValue("id");
        if (rawId.Equals(""))
        {
            //return Unauthorized();
        }

        int id = Int16.Parse(rawId);
        Posts post = new Posts();
        post.UserId = id;
        post.Image = "";
        post.Post = postDto.Post;
        post.PostTime = DateTime.UtcNow;
        await _posts.CreatePost(post);
        return CreatedAtAction(nameof(GetById), post);
    }
    
    [HttpGet("id")]
    [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Users), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        return post == null ? NotFound() : Ok(post);
    }
}
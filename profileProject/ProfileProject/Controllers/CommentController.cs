using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileProject.Data;
using ProfileProject.Models;
using ProfileProject.Services;

namespace ProfileProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ProfileDbContext _context;
    private readonly IComment _comment;
    private readonly IConfiguration _configuration;

    public CommentController(IConfiguration configuration,ProfileDbContext context, IComment comment)
    {
        _configuration = configuration;
        _context = context;
        _comment = comment;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentDto>> GetComments(int postId)
    {
        var commentDtos =  _comment.GetCommentsByPostId(postId).Result;
        return commentDtos;
    }

    [HttpPost,Authorize]
    public async Task<IActionResult> CreateComment(CommentPostDto commentPostDto)
    {
        string rawId = HttpContext.User.FindFirstValue("id");
        if (rawId.Equals(""))
        {
            //return Unauthorized();
        }

        int id = Int16.Parse(rawId);
        Comments comments = new Comments()
        {
            UserId = id,
            CommentId = 0,
            Comment = commentPostDto.Comment,
            CommentTime = DateTime.UtcNow,
            Delete = false,
            Image = "",
            PostId = commentPostDto.PostId
        };
        await _comment.CreateComment(comments);
        return CreatedAtAction(nameof(GetById), comments);
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return comment == null ? NotFound() : Ok(comment);
    }
}
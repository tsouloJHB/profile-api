using Microsoft.EntityFrameworkCore;
using ProfileProject.Data;
using ProfileProject.Models;

namespace ProfileProject.Services;

public class Comment : IComment
{
    private readonly ProfileDbContext _context;

    public Comment(ProfileDbContext context)
    {
        _context = context;
    }

    public async Task<Comments> CreateComment(Comments comments)
    {
        Comments comment = comments;
        await _context.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async  Task<List<CommentDto>> GetCommentsByPostId(int postId)
    {
        //var comments = await _context.Comments.FindAsync(postId);
        //create Dto object
        var comments =  await _context.Comments.Where( c =>  c.PostId.Equals(postId))
            .OrderBy(c => c.CommentTime).ToArrayAsync();
        List<CommentDto> commentDtos = new List<CommentDto>();
    
         foreach (var item in comments)
         {
             var user = await _context.users.FindAsync(item.UserId);
             CommentDto commentDto = new CommentDto()
             {
                 name = user.name,
                 surname = user.surname,
                 email = user.email,
                 ProfileImage = user.image,
                 CommentTime = item.CommentTime,
                 Comment = item.Comment,
                 Image = item.Image,
             };
             commentDtos.Add(commentDto);
           
         }

        return commentDtos;
       
    }
}
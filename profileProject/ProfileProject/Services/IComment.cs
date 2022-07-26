using ProfileProject.Models;

namespace ProfileProject.Services;

public interface IComment
{
    public Task<Comments> CreateComment(Comments comments);
    public Task<List<CommentDto>>  GetCommentsByPostId(int postId);
}
using BlogApp.Entity;

namespace BlogApp.Data.Abstrack
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void AddComment(Comment comment);
    }
}
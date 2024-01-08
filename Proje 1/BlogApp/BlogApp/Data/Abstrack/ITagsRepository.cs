using BlogApp.Entity;

namespace BlogApp.Data.Abstrack
{
    public interface ITagsRepository
    {
        IQueryable<Tag> Tags { get; }
        void CreateTag(Tag tags);
    }
}
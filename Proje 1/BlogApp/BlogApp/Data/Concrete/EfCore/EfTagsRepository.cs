using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfTagsRepository : ITagsRepository
    {
        private BlogContext _context;
        public EfTagsRepository(BlogContext context)
        {
            _context = context;
        }



        public IQueryable<Tag> Tags => _context.Tags;

        public void CreateTag(Tag tag)  //burada ekleme işlemi yapılacak
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }
    }
}
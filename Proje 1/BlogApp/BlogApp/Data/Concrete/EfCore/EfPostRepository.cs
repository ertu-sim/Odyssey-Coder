using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete
{
    public class EfPostRepository : IPostRepository
    {
        private BlogContext _context;
        public EfPostRepository(BlogContext context)
        {
            _context = context;
        }



        public IQueryable<Post> Posts => _context.Posts;

        public void CreatePost(Post post)  //burada ekleme işlemi yapılacak
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }
    }
}
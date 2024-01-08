using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;

namespace BlogApp.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private BlogContext _context;
        public EfUserRepository(BlogContext context)
        {
            _context = context;
        }





        public IQueryable<User> Users => _context.Users;


        public void CreateUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }








    }
}
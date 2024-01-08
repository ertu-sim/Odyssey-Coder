using BlogApp.Entity;
using BlogApp.Models;

namespace BlogApp.Data.Abstrack
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void CreateUser(User User);


    }
}
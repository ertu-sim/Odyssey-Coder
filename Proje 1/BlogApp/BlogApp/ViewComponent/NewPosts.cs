using System.ComponentModel;
using BlogApp.Data.Abstrack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.ViewComponents
{
    public class NewPosts : ViewComponent
    {
        private readonly IPostRepository _postRepository;

        public NewPosts(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        public IViewComponentResult Invoke()
        {
            var list = _postRepository.Posts.OrderByDescending(p => p.PublishedOn).Take(5).ToList(); // burada yayınlanma tarihine göre 5 tane al gibi bir method oluşturduk
            return View(list);
        }

    }
}
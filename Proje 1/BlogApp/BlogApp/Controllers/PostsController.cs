using System.Security.Claims;
using BlogApp.Data.Abstrack;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Conrollers
{
    public class PostsController : Controller
    {
        private IPostRepository _postRepository;
        private ITagsRepository _tagRepository;
        private ICommentRepository _commentRepository;
        public PostsController(IPostRepository postRepository, ITagsRepository tagRepository, ICommentRepository commentRepository)
        {
            _tagRepository = tagRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public async Task<IActionResult> Index(string tag)
        {
            var claims = User.Claims;
            var posts = _postRepository.Posts;

            if (!string.IsNullOrEmpty(tag))
            {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostViewModel { Posts = await posts.ToListAsync() });
        }


        public async Task<IActionResult> Details(string url)
        {
            var detail = await _postRepository.Posts.Include(p => p.Tags).Include(p => p.comments).ThenInclude(p => p.User).FirstOrDefaultAsync(p => p.Url == url);
            // var detail = await _postRepository.Posts.Include(t => t.Tags).FirstOrDefaultAsync(p => p.PostId == id);
            return View(detail);
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);
            var entity = new Comment
            {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.AddComment(entity);
            return Json(new
            {
                username,
                Text,
                entity.PublishedOn,
                avatar
            });
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(PostCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (ModelState.IsValid)
            {
                _postRepository.CreatePost(new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    Url = model.Url,
                    Image = model.Image ?? "default.jpg",
                    UserId = int.Parse(userId ?? ""),
                    PublishedOn = DateTime.Now,
                    IsActive = false
                });
                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ModelState.AddModelError("", "Bilgileri lütfen boş bırakmayınız!");
            }

            return View(model);
        }

        [Authorize]
        public IActionResult List()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);
            var posts = _postRepository.Posts;
            if (string.IsNullOrEmpty(role)) //
            {
                posts = posts.Where(i => i.UserId == userId);
            }
            return View(posts.ToList());
        }




    }


}
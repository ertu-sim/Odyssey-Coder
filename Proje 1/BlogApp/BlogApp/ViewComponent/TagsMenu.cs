using BlogApp.Data.Abstrack;
using Microsoft.AspNetCore.Mvc;



namespace BlogApp.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private ITagsRepository _tagRepository;

        public TagsMenu(ITagsRepository tagsRepository)
        {
            _tagRepository = tagsRepository;
        }


        public IViewComponentResult Invoke()
        {
            return View(_tagRepository.Tags.ToList());
        }
    }
}
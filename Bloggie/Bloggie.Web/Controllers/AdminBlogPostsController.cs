using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {

        //constructor injection

        private readonly ITagRepository _tagRepository;
        public AdminBlogPostsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get all tags
            var listOfTags = await _tagRepository.GetAllAsync();

            //assign tags to new view model, so create a new view model
            var modelForBlogPostTags = new AddBlogPostRequest();

            modelForBlogPostTags.Tags = listOfTags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            return View(modelForBlogPostTags);
        }

        //to actually save the post with the selected tags and other info

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            return RedirectToAction("Add");
        }
    }
}

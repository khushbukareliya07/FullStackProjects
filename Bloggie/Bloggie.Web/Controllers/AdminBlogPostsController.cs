using Bloggie.Web.Models.Domain;
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
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
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

            //map view model to Domain model

            var blogPostDomainModel = new BlogPost();

            blogPostDomainModel.Heading = addBlogPostRequest.Heading;
            blogPostDomainModel.PageTitle = addBlogPostRequest.PageTitle;
            blogPostDomainModel.Content = addBlogPostRequest.Content;
            blogPostDomainModel.ShortDescription = addBlogPostRequest.ShortDescription;
            blogPostDomainModel.FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl;
            blogPostDomainModel.UrlHandle = addBlogPostRequest.UrlHandle;
            blogPostDomainModel.PublishedDate  = addBlogPostRequest.PublishedDate;
            blogPostDomainModel.Author  = addBlogPostRequest.Author;
            blogPostDomainModel.Visible = addBlogPostRequest.Visible;

            //tags are of IEnumerable in domain but selectListItems in VIew, so we have to loop through selected to assign those
            foreach(var sele)

            await _blogPostRepository.AddAsync();
            return RedirectToAction("Add");
        }
    }
}

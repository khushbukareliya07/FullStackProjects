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

            var blogPostDomainModel = new BlogPost
            {

                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };

            //tags are of IEnumerable in domain but selectListItems in VIew, so we have to loop through selected to assign those

            var listOfTags = new List<Tag>();
            
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            { 
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    listOfTags.Add(existingTag);
                }
            }
            blogPostDomainModel.Tags = listOfTags;

            await _blogPostRepository.AddAsync(blogPostDomainModel);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call repository
            var ListOfBlogs = await _blogPostRepository.GetAllAsync();
            return View(ListOfBlogs);
        }
    }
}

using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BlogieDbContext _blogieDbContext;//this will help us connect to the DB and perform read/write operations based on CRUD!

        public AdminTagController(BlogieDbContext blogieDbContext) 
        {
            _blogieDbContext = blogieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //mapping addtagRequest to the Tag Domain Model
            var tag = new Tag() //saving data to the tags table!
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            //_blogieDbContext.Tags.Add(new Models.Domain.Tag() { Name = addTagRequest.Name, DisplayName = addTagRequest.DisplayName });

            _blogieDbContext.Tags.Add(tag);
            _blogieDbContext.SaveChanges();

            return RedirectToAction("ListofTags");
        }

        [HttpGet]
        [ActionName("ListofTags")]
        public IActionResult ListofTags() {

            //use _DBcontext to read the Tags data from table!
            var listofTags = _blogieDbContext.Tags.ToList();
            return View(listofTags);
        }
    }
}

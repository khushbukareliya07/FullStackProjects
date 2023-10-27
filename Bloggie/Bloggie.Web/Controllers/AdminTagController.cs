using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //mapping addtagRequest to the Tag Domain Model
            var tag = new Tag() //saving data to the tags table!
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            //_blogieDbContext.Tags.Add(new Models.Domain.Tag() { Name = addTagRequest.Name, DisplayName = addTagRequest.DisplayName });

            await _blogieDbContext.Tags.AddAsync(tag);
            await _blogieDbContext.SaveChangesAsync();

            return RedirectToAction("ListofTags");
        }

        [HttpGet]
        [ActionName("ListofTags")]
        public async Task<IActionResult> ListofTags() {

            //use _DBcontext to read the Tags data from table!
            var listofTags = await _blogieDbContext.Tags.ToListAsync();
            return View(listofTags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) //name same as what defined in the asp-route-id!
        {

            //1st Method
            //var tag = _blogieDbContext.Tags.Find(id);

            //2nd Method
            var tag = await _blogieDbContext.Tags.SingleOrDefaultAsync(t => t.Id == id); //or also can use firstordefault

            //ModelBinding
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagRequest);

            }
            return View(null);


        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //mapping addtagRequest to the Tag Domain Model
            var updatedTag = new Tag() //saving data to the tags table!
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            //_blogieDbContext.Tags.Add(new Models.Domain.Tag() { Name = addTagRequest.Name, DisplayName = addTagRequest.DisplayName });

            var existingTag = await _blogieDbContext.Tags.FindAsync(updatedTag.Id);

            if(existingTag != null)
            {
                existingTag.Name = updatedTag.Name;
                existingTag.DisplayName = updatedTag.DisplayName;

                //save changes
                await _blogieDbContext.SaveChangesAsync();

                //show success notification
                return RedirectToAction("ListofTags"); //back to the list page
            }
            //show Failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id }); //stay on edit page with id passed !
            
        }

        //extra method for Delete button on List Oftags page
        //[HttpPost]
        //public IActionResult Delete(Guid id)
        //{
        //    Console.WriteLine("Inside the Delete Method! -- 1");
        //    var existingTag = _blogieDbContext.Tags.SingleOrDefault(t => t.Id == id);

        //    if (existingTag != null)
        //    {
        //        _blogieDbContext.Tags.Remove(existingTag);
        //        _blogieDbContext.SaveChanges();
        //        Console.WriteLine("Inside the Delete Method!");
        //        //sucess return to list of tags
        //        Console.WriteLine("Inside the Delete Method! -- 2");
        //        return RedirectToAction("ListofTags");
        //    }
        //    //failure to delete!
        //    Console.WriteLine("Inside the Delete Method! -- 3");
        //    return View(null);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var tag = new Tag()
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };

            if (tag != null)
            {
                _blogieDbContext.Tags.Remove(tag);
               
                await _blogieDbContext.SaveChangesAsync();

                //sucess return to list of tags
                return RedirectToAction("ListofTags");
            }
            //failure to delete!

            return RedirectToAction("Edit", new { Id = editTagRequest.Id });
        }
    }
}

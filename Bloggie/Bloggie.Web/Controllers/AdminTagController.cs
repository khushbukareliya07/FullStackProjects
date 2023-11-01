using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
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

            await tagRepository.AddAsync(tag);

            return RedirectToAction("ListofTags");
        }

        [HttpGet]
        [ActionName("ListofTags")]
        public async Task<IActionResult> ListofTags() {

            //use _DBcontext to read the Tags data from table!
            var listofTags = await tagRepository.GetAllAsync();
            return View(listofTags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) //name same as what defined in the asp-route-id!
        {

           var updatedTag = await tagRepository.GetAsync(id);

            //ModelBinding
            if (updatedTag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = updatedTag.Id,
                    Name = updatedTag.Name,
                    DisplayName = updatedTag.DisplayName,
                };
                return View(editTagRequest);

            }
            return View(null);
        }

        [HttpPost]
        
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //mapping addtagRequest to the Tag Domain Model
            var newTag = new Tag() //saving data to the tags table!
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,
            };
            //_blogieDbContext.Tags.Add(new Models.Domain.Tag() { Name = addTagRequest.Name, DisplayName = addTagRequest.DisplayName });

            var updatedTag = await tagRepository.UpdateAsync(newTag);

            if (updatedTag != null)
            {
                //show success
                return View("ListofTags");
            }
            else
            {
                //show error notification
               
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
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //sucess return to list of tags, success notification
                return RedirectToAction("ListofTags");
            }
            //failure to delete!

            return RedirectToAction("Edit", new { Id = editTagRequest.Id });
        }
    }
}

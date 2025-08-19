using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.BodyClass = "adminTag-add-bg";
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.name,
                DisplayName = addTagRequest.displayName,
            };

            _bloggieDbContext.Tags.Add(tag);
            _bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
           var tags =  _bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //1st method
            //var tag = _bloggieDbContext.Tags.Find(id);

            //2nd method

            var tag = _bloggieDbContext.Tags.FirstOrDefault(t => t.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    name = tag.Name,
                    displayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
        
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            //find the tag
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.name,
                DisplayName = editTagRequest.displayName
            };

            var existingTag = _bloggieDbContext.Tags.Find(tag.Id);
            if(existingTag != null)
            {
                //update the tag
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                _bloggieDbContext.SaveChanges();
                //show success message
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            //show error message
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var tag = _bloggieDbContext.Tags.Find(id);
            if (tag != null)
            {
                _bloggieDbContext.Tags.Remove(tag);
                _bloggieDbContext.SaveChanges();
                //show success message
                return RedirectToAction("List");
            }
            //show error message
            return RedirectToAction("List", new { error = "Tag not found" });
        }
    }
}

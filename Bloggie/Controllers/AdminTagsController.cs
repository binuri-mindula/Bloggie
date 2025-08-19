using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.name,
                DisplayName = addTagRequest.displayName,
            };

            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var tags = await _bloggieDbContext.Tags.ToListAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = _bloggieDbContext.Tags.Find(id);

            //2nd method

            var tag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);

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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            //find the tag
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.name,
                DisplayName = editTagRequest.displayName
            };

            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);
            if(existingTag != null)
            {
                //update the tag
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _bloggieDbContext.SaveChangesAsync();
                //show success message
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            //show error message
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var tag = await _bloggieDbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                _bloggieDbContext.Tags.Remove(tag);
                await _bloggieDbContext.SaveChangesAsync();
                //show success message
                return RedirectToAction("List");
            }
            //show error message
            return RedirectToAction("List", new { error = "Tag not found" });
        }
    }
}

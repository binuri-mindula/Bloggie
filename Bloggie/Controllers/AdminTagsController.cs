using Bloggie.Data;
using Bloggie.Models.Domain;
using Bloggie.Models.ViewModels;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
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

            await _tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await _tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st method
            //var tag = _bloggieDbContext.Tags.Find(id);

            //2nd method

            var tag = await _tagRepository.GetByIdAsync(id);

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


            var updateTag = await _tagRepository.UpdateAsync(tag);
            if (updateTag != null)
            {
                //show success message
            }
            else
            {
                //show error message
            }

            //show error message
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            var tag = await _tagRepository.DeleteAsync(id);

            if (tag != null)
           {
                //show success message
                return RedirectToAction("List");
            }

            //show error message
            return RedirectToAction("List", new { error = "Tag not found" });
        }
    }
}

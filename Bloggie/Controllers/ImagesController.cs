using System.Net;
using Bloggie.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("This is Get image api call");
        //}

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var imageURL = await _imageRepository.UploadAsync(formFile);

            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);

            }
            return new JsonResult(new { link = imageURL });
        }
    }
}

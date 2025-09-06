using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is Get image api call");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            return Ok("This is Post image api call");
        }
    }
}

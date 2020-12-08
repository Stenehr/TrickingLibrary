using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public VideosController(IWebHostEnvironment env)
        {
            _env = env;
        }


        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            var extension = video.FileName.Split(".").Last();
            var savePath = Path.Combine(_env.WebRootPath, $"{Path.GetRandomFileName()}.{extension}");

            await using var fileStream = new FileStream(savePath, FileMode.Create);
            await video.CopyToAsync(fileStream);


            return Ok();
        }

    }
}
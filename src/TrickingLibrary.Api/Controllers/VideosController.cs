using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

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

        [HttpGet("{video}")]
        public IActionResult GetVideo(string video)
        {
            var savePath = Path.Combine(_env.WebRootPath, video);
            return new FileStreamResult(new FileStream(savePath, FileMode.Open, FileAccess.Read), MediaTypeHeaderValue.Parse("video/*"));
        }


        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            var extension = video.FileName.Split(".").Last();
            var fileName = $"{Path.GetRandomFileName()}.{extension}";
            var savePath = Path.Combine(_env.WebRootPath, fileName);

            await using var fileStream = new FileStream(savePath, FileMode.Create);
            await video.CopyToAsync(fileStream);


            return Ok(fileName);
        }

    }
}
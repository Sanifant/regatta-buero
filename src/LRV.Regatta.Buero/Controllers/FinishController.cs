using LRV.Regatta.Buero.Atributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Service Running";
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {

                if (file.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

            return Ok();
        }
    }
}

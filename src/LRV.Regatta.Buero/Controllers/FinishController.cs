using LRV.Regatta.Buero.Atributes;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        private IStorageService _storageService;
        private IConfiguration _configuration;
        private string folderpath;

        public FinishController(IStorageService storage, IConfiguration config)
        {
            this._storageService = storage;   
            this._configuration = config;
            this.folderpath = this._configuration.GetValue<string>("ImageFolder")
        }

        [HttpGet]
        public IList<FinishObject> Get()
        {
            return this._storageService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            if (!Directory.Exists(folderpath))
            {
                // Create the directory
                Directory.CreateDirectory(folderpath);
                Console.WriteLine("Directory created successfully.");
            }
            if (file.Length > 0)
            {
                FinishObject item = new FinishObject();

                item.Name = file.FileName;
                item.Path =  file.FileName;

                this._storageService.Add(item);

                using (var stream = System.IO.File.Create(Path.Combine(folderpath, file.FileName)))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok();
        }
    }
}

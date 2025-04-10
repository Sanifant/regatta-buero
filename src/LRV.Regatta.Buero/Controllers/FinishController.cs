using LRV.Regatta.Buero.Attributes;
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
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;
        private readonly string folderpath;

        public FinishController(IStorageService storage, IConfiguration config)
        {
            this._storageService = storage;   
            this._configuration = config;
            this.folderpath = this._configuration.GetValue<string>("ImageFolder");
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
                Console.WriteLine($"\tDirectory {folderpath} created successfully.");
                Console.WriteLine("");
            }

            if (file.Length > 0)
            {
                FinishObject item = new FinishObject();

                item.Name = Path.GetFileNameWithoutExtension(file.FileName);
                item.Path =  file.FileName;

                this._storageService.Add(item);
                string filePath = Path.Combine(folderpath, file.FileName);
                Console.WriteLine($"\tSaving file to {filePath}.");

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok();
        }
    }
}

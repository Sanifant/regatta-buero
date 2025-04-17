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
        private readonly IFinishService _finishService;
        private readonly IConfiguration _configuration;
        private readonly string folderpath;

        public FinishController(IFinishService storage, IConfiguration config)
        {
            this._finishService = storage;   
            this._configuration = config;
            this.folderpath = this._configuration.GetValue<string>("ImageFolder");
        }

        /// <summary>
        /// Processes the GET HTTP Verb
        /// </summary>
        /// <returns>List of all Finish Objects</returns>
        [HttpGet]
        public IList<FinishObject> Get()
        {
            return this._finishService.GetAll();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] ImageUploadRequest file)
        {
            if (!Directory.Exists(folderpath))
            {
                // Create the directory
                Directory.CreateDirectory(folderpath);
                Console.WriteLine($"\tDirectory {folderpath} created successfully.");
                Console.WriteLine("");
            }

            if (file.File.Length > 0)
            {
                FinishObject item = new FinishObject();

                item.Id = this._finishService.GetAll().Count + 1;
                item.Name = Path.GetFileNameWithoutExtension(file.File.FileName);
                item.Path =  file.File.FileName;

                this._finishService.Add(item);
                string filePath = Path.Combine(folderpath, file.File.FileName);
                Console.WriteLine($"\tSaving file to {filePath}.");

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.File.CopyToAsync(stream);
                }
            }

            return Ok();
        }
    }
}

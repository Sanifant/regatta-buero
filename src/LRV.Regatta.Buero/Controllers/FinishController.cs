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
        private const string Key = "ImageFolder";
        private readonly IFinishService _finishService;
        private readonly IConfiguration _configuration;
        private readonly string folderpath;

        public FinishController(IFinishService storage, IConfiguration config)
        {
            this._finishService = storage;
            this._configuration = config;
            this.folderpath = String.IsNullOrEmpty(Environment.GetEnvironmentVariable(Key)) ?
                Environment.GetEnvironmentVariable(Key) :
                this._configuration.GetValue<string>(Key);
        }

        /// <summary>
        /// Processes the GET HTTP Verb
        /// </summary>
        /// <returns>List of all Finish Objects</returns>
        [HttpGet]
        public IList<FinishObject> Get()
        {
            return this._finishService.GetAllFinishObject();
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

            FinishObject item = new FinishObject();

            item.Id = this._finishService.GetAllFinishObject().Count + 1;
            item.Name = $"Zieleinlauf {file.FinishTime:dd.MM.yyyy HH:mm:ss.fff}";
            item.FirstPath = file.FirstPhotoFile.FileName;
            item.SecondPath = file.SecondPhotoFile.FileName;


            string filePath = Path.Combine(folderpath, file.FirstPhotoFile.FileName);
            Console.WriteLine($"\tSaving file to {filePath}.");

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.FirstPhotoFile.CopyToAsync(stream);
            }
            filePath = Path.Combine(folderpath, file.SecondPhotoFile.FileName);
            Console.WriteLine($"\tSaving file to {filePath}.");

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.FirstPhotoFile.CopyToAsync(stream);
            }

            this._finishService.AddFinishObject(item);

            return Ok();
        }


        [HttpDelete()]
        public IActionResult Delete()
        {
            this._finishService.DeleteAllFinishObject();

            foreach (var file in Directory.GetFiles(folderpath))
            {
                System.IO.File.Delete(file);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = this._finishService.GetAllFinishObject().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                this._finishService.DeleteFinishObject(item);
                System.IO.File.Delete(Path.Combine(folderpath, item.FirstPath));
                System.IO.File.Delete(Path.Combine(folderpath, item.SecondPath));
            }
            return Ok();
        }
    }
}

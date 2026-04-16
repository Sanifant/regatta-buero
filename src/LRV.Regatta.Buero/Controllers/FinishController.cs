using LRV.Regatta.Buero.Attributes;
using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers
{
    /// <summary>
    /// FinishController class that handles HTTP requests related to finish line data in the regatta management system. This controller includes endpoints for retrieving all finish objects, uploading new finish objects with associated images, and deleting finish objects. The controller uses the IFinishService to manage finish line data and relies on configuration settings for file storage paths. It also includes an API key attribute for securing the endpoints.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        private const string Key = "ImageFolder";
        private readonly IFinishService _finishService;
        private readonly IConfiguration _configuration;
        private readonly string folderpath;

        /// <summary>
        /// Constructor for the FinishController class, initializing the IFinishService and IConfiguration dependencies. This constructor sets up the necessary services for managing finish line data and accessing configuration settings, as well as determining the folder path for storing uploaded images based on environment variables or configuration values.
        /// </summary>
        /// <param name="storage">The IFinishService instance for managing finish line data.</param>
        /// <param name="config">The IConfiguration instance for accessing configuration settings.</param>
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

        /// <summary>
        /// Processes the DELETE HTTP Verb, deleting all Finish Objects and their associated image files from the server. This method clears the in-memory collection of finish line data and removes all files from the specified folder path, effectively resetting the finish line information in the regatta management system.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
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

        /// <summary>
        /// Processes the DELETE HTTP Verb for a specific Finish Object, deleting the object and its associated image files from the server. This method removes the specified finish line data from the in-memory collection and deletes the corresponding image files from the folder path.
        /// </summary>
        /// <param name="id">The ID of the Finish Object to delete.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
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

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

        public FinishController(IStorageService storage, IConfiguration config)
        {
            this._storageService = storage;   
            this._configuration = config;
        }

        [HttpGet]
        public IList<FinishObject> Get()
        {
            return this._storageService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
                if (file.Length > 0)
                {
                    FinishObject item = new FinishObject();

                    item.Name = file.FileName;
                    item.Path =  Path.Combine(this._configuration.GetValue<string>("ImageFolder"), file.FileName);

                this._storageService.Add(item);

                    using (var stream = System.IO.File.Create(item.Path))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageUploadRequest
    {
        [FromForm]
        public IFormFile File { get; set; }

        [FromForm]
        public string Description { get; set; }
    }
}

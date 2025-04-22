using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageUploadRequest
    {
        [FromForm]
        public DateTime FinishTime { get; set; }

        [FromForm]
        public IFormFile FirstPhotoFile { get; set; }

        [FromForm]
        public IFormFile SecondPhotoFile { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageUploadRequest
    {
        /// <summary>
        /// Gets or sets the finish time for the image upload request. This property is decorated with the [FromForm] attribute, indicating that it should be bound from form data in an HTTP request. The FinishTime property allows clients to specify the time associated with the uploaded images, enabling the application to manage and analyze finish line data effectively in the context of a regatta management system.
        /// </summary>
        [FromForm]
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// Gets or sets the first photo file for the image upload request. This property is decorated with the [FromForm] attribute, indicating that it should be bound from form data in an HTTP request. The FirstPhotoFile property allows clients to upload an image file associated with the finish line data, enabling the application to store and manage visual information related to finish line events in the regatta management system.
        /// </summary>
        [FromForm]
        public IFormFile FirstPhotoFile { get; set; }

        /// <summary>
        /// Gets or sets the second photo file for the image upload request. This property is decorated with the [FromForm] attribute, indicating that it should be bound from form data in an HTTP request. The SecondPhotoFile property allows clients to upload a second image file associated with the finish line data, enabling the application to store and manage additional visual information related to finish line events in the regatta management system.
        /// </summary>
        [FromForm]
        public IFormFile SecondPhotoFile { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Application.Contract.ProductPicture {
    public class CreateProductPicture {
        [Range(1,10000,ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [FileExtensionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public IFormFile? Picture { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }

        public List<ProductViewModel> Products { get; set; }

    }
}

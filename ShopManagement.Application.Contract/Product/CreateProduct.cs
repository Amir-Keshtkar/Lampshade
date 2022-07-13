using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Contract.Product;

public class CreateProduct {
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Name { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Code { get;  set; }
    
    [DataType(DataType.Currency,ErrorMessage = ValidationMessages.NotInteger),Required(ErrorMessage = ValidationMessages.IsRequired)]
    public double UnitPrice { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? ShortDescription { get;  set; }
    
    public string? Description { get;  set; }

    [FileExtensionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile? Picture { get;  set; }

    public string? PictureAlt { get;  set; }
    public string? PictureTitle { get;  set; }

    [Range(1 , 10000, ErrorMessage = ValidationMessages.IsRequired)]
    public long CategoryId { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Slug { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Keywords { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    [MaxLength(150,ErrorMessage =ValidationMessages.NotInRange)]
    public string? MetaDescription { get;  set; }

    public List<ProductCategoryViewModel>? ProductCategories { get; set; }
}
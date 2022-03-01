using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
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
    public string? Picture { get;  set; }
    public string? PictureAlt { get;  set; }
    public string? PictureTitle { get;  set; }

    [Range(1 , 10000, ErrorMessage = ValidationMessages.IsRequired)]
    public long CategoryId { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Slug { get;  set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? Keywords { get;  set; }
    
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string? MetaDescription { get;  set; }

    public List<ProductCategoryViewModel>? ProductCategories { get; set; }
}
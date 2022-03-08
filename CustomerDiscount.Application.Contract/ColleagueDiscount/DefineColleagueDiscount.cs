using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace DiscountManagement.Application.Contract.ColleagueDiscount {
    public class DefineColleagueDiscount {
        
        [Range(1,100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1,99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }
        
        public List<ProductViewModel> Products{ get; set; }
        
        public List<ProductCategoryViewModel> Categories{ get; set; }

    }
}

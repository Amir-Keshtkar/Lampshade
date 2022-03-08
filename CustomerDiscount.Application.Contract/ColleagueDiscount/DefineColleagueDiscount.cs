using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace DiscountManagement.Application.Contract.ColleagueDiscount {
    public class DefineColleagueDiscount {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public List<ProductViewModel> Products{ get; set; }
        public List<ProductCategoryViewModel> Categories{ get; set; }

    }
}

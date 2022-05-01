using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions {
    public class ShopPermissionExposer: IPermissionExposer {
        public Dictionary<string, List<PermissionDto>> Expose () {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Products", new List<PermissionDto> {
                        new PermissionDto(ShopPermissions.ListProduct, "ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts, "SearchProducts"),
                        new PermissionDto(ShopPermissions.EditProduct, "EditProduct"),
                        new PermissionDto(ShopPermissions.CreateProduct, "CreateProduct"),
                    }
                },
                {
                "ProductCategory", new List<PermissionDto> {
                    new PermissionDto(ShopPermissions.ListProductCategories, "ListProductCategories"),
                    new PermissionDto(ShopPermissions.SearchProductCategories, "SearchProductCategories"),
                    new PermissionDto(ShopPermissions.EditProductCategory, "EditProductCategory"),
                    new PermissionDto(ShopPermissions.CreateProductCategory, "CreateProductCategory"),
                }
            }
            };
        }
    }
}

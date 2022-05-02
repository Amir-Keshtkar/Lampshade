using System.Text;
using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.Configuration.Permissions {
    public class ShopPermissionExposer: IPermissionExposer {
        public Dictionary<string, List<PermissionDto>> Expose () {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Products", new List<PermissionDto> {
                        new(ShopPermissions.ListProduct, "لیست محصولات"),
                        new(ShopPermissions.SearchProducts, "جستجو در محصولات"),
                        new(ShopPermissions.EditProduct, "ویرایش محصول"),
                        new(ShopPermissions.CreateProduct, "ساخت محصول"),
                    }
                },
                {
                    "Product Category", new List<PermissionDto> {
                        new(ShopPermissions.ListProductCategories, "لیست گروه محصولات"),
                        new(ShopPermissions.SearchProductCategories, "جستجو در گروه محصولات"),
                        new(ShopPermissions.EditProductCategory, "ویرایش گروه محصول"),
                        new(ShopPermissions.CreateProductCategory, "ساخت گروه محصول"),
                    }
                },
                {
                    "Product Picture", new List<PermissionDto> {
                        new(ShopPermissions.ListProductPictures, "لیست عکس ها"),
                        new(ShopPermissions.SearchProductPictures, "جستجو در عکس ها"),
                        new(ShopPermissions.EditProductPicture, "ویرایش عکس"),
                        new(ShopPermissions.CreateProductPicture, "ساخت عکس"),
                        new(ShopPermissions.RemoveProductPicture, "حذف عکس"),
                    }
                },
                {
                    "Slide", new List<PermissionDto> {
                        new(ShopPermissions.ListSlides, "لیست اسلاید ها"),
                        new(ShopPermissions.SearchSlides, "جستجو در اسلاید ها"),
                        new(ShopPermissions.EditSlide, "ویرایش اسلاید"),
                        new(ShopPermissions.CreateSlide, "ساخت اسلاید"),
                        new(ShopPermissions.RemoveSlide, "حذف اسلاید"),
                    }
                }
            };
        }
    }
}

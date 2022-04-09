using _01_LampshadeQuery;
using _01_LampshadeQuery.Contract.ArticleCategory;
using _01_LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents {
    public class HeaderViewComponent: ViewComponent {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;

        public HeaderViewComponent (IArticleCategoryQuery articleCategoryQuery, IProductCategoryQuery productCategoryQuery) {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke () {
            var menuModel = new MenuModel {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View(menuModel);
        }
    }
}

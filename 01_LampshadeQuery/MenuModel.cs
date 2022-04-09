﻿using _01_LampshadeQuery.Contract.ArticleCategory;
using _01_LampshadeQuery.Contract.ProductCategory;

namespace _01_LampshadeQuery {
    public class MenuModel {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}

namespace _01_LampshadeQuery.Contract.ProductCategory {
    public interface IProductCategoryQuery {
        List<ProductCategoryQueryModel> GetProductCategories ();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts ();
        ProductCategoryQueryModel GetProductCategoryWithProductsBySlug(string slug);
    }
}

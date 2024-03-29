﻿using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategory {
    public interface IProductCategoryApplication {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel command);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories ();
    }
}

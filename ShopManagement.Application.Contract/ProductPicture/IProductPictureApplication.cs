﻿using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductPicture {
    public interface IProductPictureApplication {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        EditProductPicture? GetDetails(long id);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}

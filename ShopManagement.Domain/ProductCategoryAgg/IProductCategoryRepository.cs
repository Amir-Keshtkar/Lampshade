using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg {
    public interface IProductCategoryRepository : IRepository<long , ProductCategory> {
        
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}

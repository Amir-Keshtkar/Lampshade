using System.Runtime.InteropServices.ComTypes;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application {
    public class ProductCategoryApplication: IProductCategoryApplication {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductCategoryApplication (IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader) {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create (CreateProductCategory command) {
            var operation = new OperationResult();
            if(_productCategoryRepository.Exists(x => x.Name == command.Name)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }

            var slug = command.Slug.Slugify();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);
            var productCategory = new ProductCategory(command.Name, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit (EditProductCategory command) {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetById(command.Id);
            if(productCategory == null) {
                operation.Failed(ApplicationMessages.RecordNotFound);
            }

            if(_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id)) {
                operation.Failed(ApplicationMessages.DuplicatedMessage);
            }

            var slug = command.Slug.Slugify();
            var picturePath = $"{command.Slug}";
            var fileName = _fileUploader.Upload(command.Picture, picturePath);

            productCategory.Edit(command.Name, command.Description, fileName, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductCategory GetDetails (long id) {
            return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories () {
            return _productCategoryRepository.GetProductCategories();
        }

        public List<ProductCategoryViewModel> Search (ProductCategorySearchModel command) {
            return _productCategoryRepository.Search(command);
        }
    }
}
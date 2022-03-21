using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application {
    public class ProductPictureApplication: IProductPictureApplication {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication (IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository) {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create (CreateProductPicture command) {
            var operation = new OperationResult();
            // if(_productPictureRepository.Exists(x => x.Picture == command.Picture && x.Id != command.ProductId)) {
            //     return operation.Failed(ApplicationMessages.DuplicatedMessage);
            // }
            
            var product = _productRepository.GetProductWithCategoryById(command.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var productPicture = new ProductPicture(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit (EditProductPicture command) {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategoryById(command.Id);
            if(productPicture == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            // if(_productPictureRepository.Exists(x =>
            //        x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id == command.Id)) {
            //     return operation.Failed(ApplicationMessages.DuplicatedMessage);
            // }
            
            var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProductPicture? GetDetails (long id) {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove (long id) {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetById(id);
            if(productPicture == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore (long id) {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetById(id);
            if(productPicture == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ProductPictureViewModel> Search (ProductPictureSearchModel searchModel) {
            return _productPictureRepository.Search(searchModel);
        }
    }
}

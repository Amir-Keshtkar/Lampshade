using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository {
    public class CommentRepository: RepositoryBase<long, Comment>, ICommentRepository {
        private readonly ShopContext _shopContext;

        public CommentRepository (ShopContext context) : base(context) {
            _shopContext = context;
        }

        public List<CommentViewModel> Search (CommentSearchModel command) {
            var query = _shopContext.Comments
                .Include(x => x.Product)
                .Select(x => new CommentViewModel {
                    Id = x.Id,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    Message = x.Message,
                    Email = x.Email,
                    Name = x.Name,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                });

            if(!string.IsNullOrWhiteSpace(command.Email)) {
                query = query.Where(x => x.Email.Contains(command.Email));
            }
            if(!string.IsNullOrWhiteSpace(command.Name)) {
                query = query.Where(x => x.Name.Contains(command.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}

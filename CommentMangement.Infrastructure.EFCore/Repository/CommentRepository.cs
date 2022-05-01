using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.Repository {
    public class CommentRepository: RepositoryBase<long, Comment>, ICommentRepository {
        private readonly CommentContext _commentContext;

        public CommentRepository (CommentContext context) : base(context) {
            _commentContext = context;
        }

        public List<CommentViewModel> Search (CommentSearchModel command) {
            var query = _commentContext.Comments
                .Select(x => new CommentViewModel {
                    Id = x.Id,
                    IsCanceled = x.IsCanceled,
                    IsConfirmed = x.IsConfirmed,
                    RecordOwnerId = x.RecordOwnerId,
                    Website = x.Website,
                    Type = x.Type,
                    Message = x.Message,
                    Email = x.Email,
                    Name = x.Name,
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

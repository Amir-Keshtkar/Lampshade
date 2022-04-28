using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrastructure.EfCore.Repository {
    public class RoleRepository: RepositoryBase<long, Role>, IRoleRepository {
        private readonly AccountContext _context;

        public RoleRepository (AccountContext context) : base(context) {
            _context = context;
        }

        public EditRole GetDetails (long id) {
            return _context.Roles.Select(x => new EditRole {
                Name = x.Name,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id)!;
        }
    }
}

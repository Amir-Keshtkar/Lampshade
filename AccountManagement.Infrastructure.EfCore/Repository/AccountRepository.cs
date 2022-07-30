using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository {
    public class AccountRepository: RepositoryBase<long, Account>, IAccountRepository {
        private readonly AccountContext _accountContext;

        public AccountRepository (AccountContext context) : base(context) {
            _accountContext = context;
        }

        public EditAccount GetDetails (long id) {
            return _accountContext.Accounts.Select(x => new EditAccount {
                FullName = x.FullName,
                UserName = x.UserName,
                Mobile = x.Mobile,
                RoleId = x.RoleId,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id)!;
        }

        public List<AccountViewModel> Search (AccountSearchModel searchModel) {
            var query = _accountContext.Accounts
                .Include(x => x.Role)
                .Select(x => new AccountViewModel {
                    Id = x.Id,
                    FullName = x.FullName,
                    UserName = x.UserName,
                    Mobile = x.Mobile,
                    Role = x.Role.Name,
                    RoleId = x.RoleId,
                    ProfilePhoto = x.ProfilePhoto,
                    CreationDate = x.CreationDate.ToFarsi()
                });
            if(!string.IsNullOrWhiteSpace(searchModel.FullName)) {
                query = query.Where(x => x.FullName.Contains(searchModel.FullName));
            }
            if(!string.IsNullOrWhiteSpace(searchModel.UserName)) {
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));
            }
            if(!string.IsNullOrWhiteSpace(searchModel.Mobile)) {
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
            }
            if(searchModel.RoleId > 0) {
                query = query.Where(x => x.RoleId == searchModel.RoleId);
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Account GetByUsername (string username) {
            return _accountContext.Accounts.Include(x=>x.Role).FirstOrDefault(x => x.UserName == username)!;
        }

        public List<AccountViewModel> GetAccounts() {
            return _accountContext.Accounts.Select(x=> new AccountViewModel {
                Id = x.Id,
                FullName=x.FullName
            }).ToList();
        }
    }
}

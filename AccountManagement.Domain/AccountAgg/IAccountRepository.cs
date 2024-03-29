﻿using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg {
    public interface IAccountRepository: IRepository<long, Account> {
        EditAccount GetDetails (long id);
        List<AccountViewModel> Search (AccountSearchModel searchModel);
        Account GetByUsername(string username);
        List<AccountViewModel> GetAccounts();
    }
}

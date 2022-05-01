using System.Linq.Expressions;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using Newtonsoft.Json;

namespace AccountManagement.Application {
    public class AccountApplication: IAccountApplication {
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;

        public AccountApplication (IAccountRepository accountRepository, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAuthHelper authHelper, IRoleRepository roleRepository) {
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult Register (RegisterAccount command) {
            var operation = new OperationResult();
            if(command.Password != command.RePassword) {
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            }
            if(_accountRepository.Exists(x => x.UserName == command.UserName || x.Mobile == command.Mobile)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            var password = _passwordHasher.Hash(command.Password);
            var path = "ProfilePictures";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            var account = new Account(command.FullName, command.UserName, password, command.Mobile,
                command.RoleId, picturePath);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit (EditAccount command) {
            var operation = new OperationResult();
            var account = _accountRepository.GetById(command.Id);
            if(account == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_accountRepository.Exists(x => (x.UserName == command.UserName || x.Mobile == command.Mobile) && x.Id != command.Id)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            var path = "ProfilePictures";
            var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
            account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword (ChangePassword command) {
            var operation = new OperationResult();
            var account = _accountRepository.GetById(command.Id);
            if(account == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(command.NewPassword != command.RePassword) {
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            }
            // var oldPassword = _passwordHasher.Hash(command.OldPassword);
            // if(account.Password != oldPassword) {
            //     return operation.Failed(ApplicationMessages.PasswordWrong);
            // }
            var password = _passwordHasher.Hash(command.NewPassword);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Login (Login command) {
            var operation = new OperationResult();
            var account = _accountRepository.GetByUsername(command.Username);
            if(account == null) {
                return operation.Failed(ApplicationMessages.WrongUserPass);
            }

            var result = _passwordHasher.Check(account.Password, command.Password);
            if(!result.Verified) {
                return operation.Failed(ApplicationMessages.PasswordWrong);
            }

            var permissions = _roleRepository.GetById(account.RoleId).Permissions
                .Select(x => x.Code).ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Role.Name,
                account.FullName, account.UserName, permissions);
            _authHelper.SignIn(authViewModel);

            return operation.Succeeded();
        }

        public EditAccount GetDetails (long id) {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> Search (AccountSearchModel searchModel) {
            return _accountRepository.Search(searchModel);
        }

        public void Logout () {
            _authHelper.SignOut();
        }
    }
}

using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public List<AccountViewModel>? Accounts;
        public AccountSearchModel? SearchModel;
        public SelectList? Roles;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication) {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet (AccountSearchModel searchModel) {
            Roles = new SelectList( _roleApplication.GetAll(), "Id", "Name");
            Accounts=_accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate () {
            var account = new CreateAccount() {
                Roles = _roleApplication.GetAll()
            };
            return Partial("./Create", account);
        }

        public JsonResult OnPostCreate (CreateAccount command) {
            var result=_accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var account=_accountApplication.GetDetails(id);
            account.Roles = _roleApplication.GetAll();
            return Partial("./Edit", account);
        }

        public JsonResult OnPostEdit (EditAccount command) {
            var result=_accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword (long id) {
            var command = new ChangePassword(){Id = id};
            return Partial("./ChangePassword", command);
        }

        public JsonResult OnPostChangePassword (ChangePassword command) {
            var result=_accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }
    }
}
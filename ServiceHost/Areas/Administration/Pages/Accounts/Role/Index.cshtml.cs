using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly IRoleApplication _roleApplication;
        public List<RoleViewModel>? Roles;

        public IndexModel(IRoleApplication roleApplication) {
            _roleApplication = roleApplication;
        }

        public void OnGet () {
            Roles = _roleApplication.GetAll();
        }

        public IActionResult OnGetCreate () {
            var account = new CreateRole();
            return Partial("./Create", account);
        }

        public JsonResult OnPostCreate (CreateRole command) {
            var result=_roleApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var role=_roleApplication.GetDetails(id);
            return Partial("./Edit", role);
        }

        public JsonResult OnPostEdit (EditRole command) {
            var result=_roleApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}

using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.Configuration.Permissions;
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
    }
}

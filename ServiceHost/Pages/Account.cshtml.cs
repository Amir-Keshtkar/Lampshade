using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class AccountModel: PageModel {
        [TempData] public string LoginMessage { get; set; }
        [TempData] public string RegisterMessage { get; set; }
        private readonly IAccountApplication _accountApplication;

        public AccountModel (IAccountApplication accountApplication) {
            _accountApplication = accountApplication;
        }

        public void OnGet (string loginMessage) {
            LoginMessage = loginMessage;
        }

        public IActionResult OnPostLogin (Login command) {
            if(!ModelState.IsValid) {
                LoginMessage = ApplicationMessages.WrongUserPass;
                return RedirectToPage("/Account");
            }
            var result = _accountApplication.Login(command);
            if(result.IsSucceeded) {
                return RedirectToPage("/Index");
            }
            LoginMessage = result.Message;
            return RedirectToPage("/Account");
        }

        public IActionResult OnGetLogout () {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRegister (RegisterAccount command) {
            if(!ModelState.IsValid) {
                RegisterMessage = ApplicationMessages.InfosNotComplete;
                return RedirectToPage("/Account");
            }
            var result = _accountApplication.Register(command);
            RegisterMessage = result.Message;
            if(result.IsSucceeded) {
                return RedirectToPage("/Account");
            }
            return RedirectToPage("/Account");
        }
    }
}

using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Nancy;
using Newtonsoft.Json;
using System.Threading;

namespace ServiceHost.Pages {
    public class AccountModel : PageModel {
        [TempData] public string LoginMessage { get; set; }
        [TempData] public string RegisterMessage { get; set; }
        public DNTCaptchaApiResponse Captcha { get; set; }
        private readonly IAccountApplication _accountApplication;
        private readonly HttpClient _client;
        private readonly IDNTCaptchaApiProvider _apiProvider;


        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;

        public AccountModel(IAccountApplication accountApplication, IOptions<DNTCaptchaOptions> captchaOptions, IDNTCaptchaValidatorService validatorService, IDNTCaptchaApiProvider apiProvider) {
            _accountApplication = accountApplication;
            _client = new HttpClient();
            _captchaOptions = captchaOptions == null ? throw new ArgumentNullException(nameof(captchaOptions)) : captchaOptions.Value;
            _validatorService = validatorService;
            _apiProvider = apiProvider;
        }

        public void OnGet(string loginMessage) {
            LoginMessage = loginMessage;

            Captcha = _apiProvider.CreateDNTCaptcha(new DNTCaptchaTagHelperHtmlAttributes {
                BackColor = "#f7f3f3",
                FontName = "Tahoma",
                FontSize = 18,
                ForeColor = "#111111",
                Language = Language.English,
                DisplayMode = DisplayMode.SumOfTwoNumbers,
                Max = 90,
                Min = 1
            });
        }


        [HttpPost, ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(ErrorMessage = "کد امنیتی نادرست است", CaptchaGeneratorLanguage = Language.English, CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbers)]
        public IActionResult OnPostLogin( Login  command) {

            if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.SumOfTwoNumbers)) {
                this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Please enter the security code as a number.");
                LoginMessage = ApplicationMessages.WrongUserPass;
                return RedirectToPage("/Account");
            }

            if (!ModelState.IsValid) {
                LoginMessage = ApplicationMessages.WrongUserPass;
                return RedirectToPage("/Account");
            }

            //Captcha
            //var secretKey= "6LdLo5cjAAAAAPSgkRndDWFLGlv2XWYVKQW_VmwH";
            //var url = "https://www.google.com/recaptcha/api/siteverify";

            //var parameteres = new Dictionary<string, string>
            //{
            //    { "secret", secretKey},
            //    { "response", command.Form["g-recaptcha-response"]}
            //};
            //var urlEncoded = new FormUrlEncodedContent(parameteres);

            //var response =  _client.PostAsync(url, urlEncoded);

            //if (response == null || !response.Result.IsSuccessStatusCode) {
            //    LoginMessage = ApplicationMessages.NotIdentifiedd;
            //    return RedirectToPage("/Account");
            //}

            //var content = response.Result.Content.ReadAsStringAsync();
            //var sendResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(content.Result);
            //if (!sendResponse.Success) {
            //    LoginMessage = ApplicationMessages.NotIdentifiedd;
            //    return RedirectToPage("/Account");
            //}

            //Login
            var result = _accountApplication.Login(command);
            if (result.IsSucceeded) {
                return RedirectToPage("/Index");
            }
            LoginMessage = result.Message;
            return RedirectToPage("/Account");
        }

        public IActionResult OnGetLogout() {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRegister(RegisterAccount command) {
            if (!ModelState.IsValid) {
                RegisterMessage = ApplicationMessages.InfosNotComplete;
                return RedirectToPage("/Account");
            }
            var result = _accountApplication.Register(command);
            RegisterMessage = result.Message;
            if (result.IsSucceeded) {
                return RedirectToPage("/Account");
            }
            return RedirectToPage("/Account");
        }
    }

    public class RecaptchaResponse {
        public bool Success { get; set; }
        public string Hostname { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonProperty("error-codes")]
        public IEnumerable<string> ErrorCodes { get; set; }
    }
}

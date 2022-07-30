using System.Security.Claims;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace _0_Framework.Application {
    public class AuthHelper : IAuthHelper {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
        }

        public void SignIn(AuthViewModel account) {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim> {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("RoleName", account.Role),
                new Claim(ClaimTypes.NameIdentifier, account.Username), // Or Use ClaimTypes.NameIdentifier
                new Claim("Permissions", permissions),
                new Claim(ClaimTypes.MobilePhone, account.Mobile)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut() {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public bool IsAuthenticated() {
            return _contextAccessor.HttpContext.User.Identity!.IsAuthenticated;
        }

        public string CurrentAccountRole() {
            return (IsAuthenticated() ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value : null)!;
        }

        public AuthViewModel CurrentAccountInfo() {
            var result = new AuthViewModel();
            if (!IsAuthenticated()) {
                return result;
            }

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId")!.Value);
            result.Username = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value);
            result.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
            result.Role = claims.FirstOrDefault(x => x.Type == "RoleName")!.Value;
            result.Mobile=claims.FirstOrDefault(x=>x.Type==ClaimTypes.MobilePhone)!.Value;
            return result;
        }

        public List<int> GetPermissions() {
            if (!IsAuthenticated()) {
                return new List<int>();
            }
            var permission = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")?.Value;
            return (permission == null ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(permission))!;
        }

        public long CurrentAccountId() {
            var accountId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "AccountId")?.Value;
            return long.Parse(accountId!);
        }
    }
}
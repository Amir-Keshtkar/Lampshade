using _0_Framework.Infrastructure;

namespace AccountManagement.Infrastructure.Configuration.Permissions {
    public class AccountPermissionExposer : IPermissionExposer{
        public Dictionary<string, List<PermissionDto>> Expose() {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Accounts", new List<PermissionDto> {
                        new PermissionDto(AccountPermissions.ListAccounts, "لیست کاربران"),
                        new PermissionDto(AccountPermissions.SearchAccounts, "جستجو در کاربران"),
                        new PermissionDto(AccountPermissions.CreateAccount, "ایجاد کاربر"),
                        new PermissionDto(AccountPermissions.EditAccount, "ویرایش کاربر"),
                        new PermissionDto(AccountPermissions.ChangePassword, "تغییر رمز"),
                    }
                }, 
                {
                    "Roles", new List<PermissionDto> {
                        new PermissionDto(AccountPermissions.ListRoles, "لیست نقش ها"),
                        new PermissionDto(AccountPermissions.CreateRole, "ایجاد نقش"),
                        new PermissionDto(AccountPermissions.EditRole, "ویرایش نقش"),
                    }
                },
            };
        }
    }
}

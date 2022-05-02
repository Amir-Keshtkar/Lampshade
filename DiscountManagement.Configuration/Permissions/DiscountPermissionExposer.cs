using _0_Framework.Infrastructure;

namespace DiscountManagement.Infrastructure.Configuration.Permissions {
    public class DiscountPermissionExposer: IPermissionExposer {
        public Dictionary<string, List<PermissionDto>> Expose() {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Colleague Discount", new List<PermissionDto> {
                        new PermissionDto(DiscountPermissions.ListColleagueDiscounts, "لیست تخفیف همکاران"),
                        new PermissionDto(DiscountPermissions.SearchColleagueDiscounts, "جستجو در تخفیف همکاران"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscounts, "ویرایش تخفیف همکار"),
                        new PermissionDto(DiscountPermissions.CreateColleagueDiscounts, "تعریف تخفیف همکار"),
                        new PermissionDto(DiscountPermissions.RemoveColleagueDiscounts, "حذف تخفیف همکار"),
                    }
                },
                {
                    "Customer Discount", new List<PermissionDto> {
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts, "لیست تخفیف مشتری"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscounts, "جستجو در تخفیف مشتری"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscounts, "ویرایش تخفیف مشتری"),
                        new PermissionDto(DiscountPermissions.CreateCustomerDiscounts, "تعریف تخفیف مشتری"),
                    }
                },
            };
        }
    }
}

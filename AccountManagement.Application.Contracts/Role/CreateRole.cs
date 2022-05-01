using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using _0_Framework.Application;
using _0_Framework.Infrastructure;

namespace AccountManagement.Application.Contracts.Role {
    public class CreateRole {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public List<int>? Permissions { get; set; }
    }
}

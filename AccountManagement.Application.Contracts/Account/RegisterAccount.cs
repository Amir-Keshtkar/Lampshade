﻿using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account {
    public class RegisterAccount {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string RePassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long RoleId { get; set; }

        public IFormFile? ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AccountManagement.Application.Contracts.Account;

public class Login {
    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Username { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string Password { get; set; }

    public string DNTCaptchaText { get; set; }
    public string DNTCaptchaToken { get; set; }
    public int DNTCaptchaInputText { get; set; }

    //public IFormCollection Form { get; set; }
}
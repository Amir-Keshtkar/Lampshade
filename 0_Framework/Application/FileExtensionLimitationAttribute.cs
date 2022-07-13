using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application {
    public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator {
        private readonly string[] _extensions;

        public FileExtensionLimitationAttribute (string[] extensions) {
            _extensions = extensions;
        }

        public override bool IsValid (object? value) {
            var file = value as IFormFile;
            if(file == null) {
                return true;
            }
            var extension = Path.GetExtension(file.Name);
            return _extensions.Contains(extension);
        }

        public void AddValidation (ClientModelValidationContext context) {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage!);
        }
    }
}

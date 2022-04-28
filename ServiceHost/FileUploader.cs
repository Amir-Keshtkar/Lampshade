using System.Text.RegularExpressions;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ServiceHost {
    public class FileUploader: IFileUploader {
        private readonly IWebHostEnvironment _environment;

        public FileUploader (IWebHostEnvironment environment) {
            _environment = environment;
        }

        public string Upload (IFormFile file, string path) {
            if(file == null) {
                return "";
            }
            var directoryPath = $"{_environment.WebRootPath}//UploadedPictures//{path}";
            if(!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = $"{DateTime.Now.ToFileName()}-{Regex.Replace(file.FileName, @"[#]", string.Empty)}";

            var filePath = $"{directoryPath}//{fileName}";

            _ = Uploader(file, filePath);

            return $"{path}/{fileName}";
        }

        private static async Task Uploader (IFormFile file, string filePath) {
            await using var output = File.Create(filePath);
            await file.CopyToAsync(output);
            // await using var fileStream = new FileStream(filePath, FileMode.Create);
            // await file.CopyToAsync(fileStream);
        }
    }
}

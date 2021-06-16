using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace backend_project_asp.Areas.AdminPanel.Utils
{
    public static class FileUtil
    {
        public static async Task<string> GenerateFileAsync(string folderPath, IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}

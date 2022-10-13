using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Utilities.File
{
    public class FileService : IFileService
    {
        public async Task<string> FileSave(string filePath, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            string fileName = Guid.NewGuid().ToString() + extension;
            var path = $"{filePath}{fileName}";
            using (var stream = System.IO.File.Create(path))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}

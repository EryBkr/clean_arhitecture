using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Utilities.File
{
    public interface IFileService
    {
        Task<string> FileSaveAsync(string filePath, IFormFile file);
        void FileDeleteToServer(string path);
    }
}

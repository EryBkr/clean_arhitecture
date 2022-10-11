using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IFileService
    {
        Task<string> FileSave(string filePath, IFormFile file);
    }
}

using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            //Kendi yazdığımız middleware'i app'e extension olarak ekleyeceğiz
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

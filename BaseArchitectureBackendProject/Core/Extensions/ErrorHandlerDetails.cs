﻿using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions
{
    //Hata mesajlarını wrap lemek için oluşturduk
    public class ErrorHandlerDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        //ToString metodu json türüne çevirecek şekilde eziyoruz
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ValidationErrorDetails : ErrorHandlerDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}

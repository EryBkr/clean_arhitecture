using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Authentication
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterAuthDto authDto);
        Task<IDataResult<Token>> LoginAsync(LoginAuthDto loginDto);
    }
}

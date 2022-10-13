using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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
        IResult Register(RegisterAuthDto authDto);
        string Login(LoginAuthDto loginDto);
    }
}

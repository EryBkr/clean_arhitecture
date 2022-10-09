using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto authDto, int imageSize=2);
        string Login(LoginAuthDto loginDto);
    }
}

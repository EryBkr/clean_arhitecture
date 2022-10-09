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
        string Register(RegisterAuthDto authDto);
        string Login(LoginAuthDto loginDto);
    }
}

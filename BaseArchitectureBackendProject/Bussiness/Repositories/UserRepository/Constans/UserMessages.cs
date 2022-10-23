using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserRepository.Constans
{
    public class UserMessages
    {
        public const string UpdatedUser = "Kullanıcı kaydı başarıyla güncellendi";
        public const string DeletedUser = "Kullanıcı kaydı başarıyla silindi";
        public static string WrongCurrentPassword = "Mevcut şifrenizi yanlış girdiniz";
        public static string ChangedPassword = "Şifre değişimi başarılı";
    }
}

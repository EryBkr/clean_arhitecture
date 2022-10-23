using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.UserOperationClaimRepository.Constant
{
    public class UserOperationClaimMessages
    {
        public const string Added = "Yetki ataması başarıyla oluşturuldu";
        public const string Updated = "Yetki ataması başarıyla güncellendi";
        public const string Deleted = "Yetki ataması başarıyla silindi";
        public const string IsOperationSetAvaible = "Yetki ataması daha önce gerçekleştirilmiş";
        public const string OperationClaimExist = "Seçtiğiniz yetki bilgisi yetkilerde bulunmuyor";
        public const string UserNotExist = "Seçtiğiniz kullanıcı bulunamadı";
    }
}

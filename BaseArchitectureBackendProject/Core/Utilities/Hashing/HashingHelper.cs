using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Hashing
{
    public class HashingHelper
    {
        //Parolanın hash operasyonu
        public static void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }

        //Parolayı hash'leyip doğru olup olmadığını teyit ediyoruz
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    //Eşleşme sağlanamadıysa
                    if (computeHash[i] != passwordHash[i])
                        return false;
                }
            }
            //Eşleşme sağlandı ise
            return true;
        }
    }
}

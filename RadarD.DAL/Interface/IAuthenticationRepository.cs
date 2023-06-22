using RadarD.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.DAL.Interface
{
    public interface IAuthenticationRepository
    {
        void RegisterUser(string email, byte[] passwordHash, byte[] passwordSalt);
        bool DoesUserExist(string email);
        User? GetUser(string email);
        void InitiateToken(User user, string jwt, DateTime expiry);

        User? GetUserByJwt(string jwt);
        void LogOutUser(User user);
    }
}

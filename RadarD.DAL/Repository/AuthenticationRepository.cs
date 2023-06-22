using RadarD.DAL.Context;
using RadarD.DAL.Data;
using RadarD.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.DAL.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly MainDBContext _dbContext;

        public AuthenticationRepository(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool DoesUserExist(string email)
        {
            var user = _dbContext.User.FirstOrDefault(x => x.Email == email);
            return user == null;
        }

        public User? GetUser(string email)
        {
            return _dbContext.User.AsQueryable().FirstOrDefault(x => x.Email == email);
        }

        public User? GetUserByJwt(string jwt)
        {
            return _dbContext.User.AsQueryable().FirstOrDefault(x => x.Jwt == jwt);
        }

        public void InitiateToken(User user, string jwt, DateTime expiry)
        {
            user.Jwt = jwt;
            user.Expiry = expiry;
            _dbContext.User.Update(user);
            _dbContext.SaveChanges();
        }

        public void LogOutUser(User user)
        {
            user.Jwt = null;
            user.Expiry = null;
            _dbContext.User.Update(user);
            _dbContext.SaveChanges();
        }

        public void RegisterUser(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            User user = new User
            {
                Email = email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Id = Guid.NewGuid()
            };
           
            _dbContext.User.Add(user);
            _dbContext.SaveChanges();
        }
    }
}

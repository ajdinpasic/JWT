using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RadarD.BLL.Data;
using RadarD.BLL.Interface;
using RadarD.DAL.Data;
using RadarD.DAL.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RadarD.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IMapper mapper, IConfiguration config)
        {
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _config = config;
        }

        public string LoginUser(UserDTO user)
        {
            var foundUser = _authenticationRepository.GetUser(user.Email);
            if (foundUser == null) throw new Exception("Wrong credentials");

            if (!VerifyPasswordHash(user.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
            {
                throw new Exception("Wrong credentials");
            }

            if (foundUser.Jwt != null) return foundUser.Jwt;

            return CreateToken(foundUser);
        }

        public void RegisterUser(UserDTO user)
        {
            if (!_authenticationRepository.DoesUserExist(user.Email)) throw new Exception("User already exists");


            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            _authenticationRepository.RegisterUser(user.Email, passwordHash, passwordSalt);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA512();
           
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            
        }
        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hmac = new HMACSHA512(passwordSalt);
            
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
            
        }

        private string CreateToken(User user)
        {

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiryDate = DateTime.Now.AddMinutes(40);
            var token = new JwtSecurityToken(
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email)
                },
                expires: expiryDate,
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            _authenticationRepository.InitiateToken(user, jwt, expiryDate);
            return jwt;
        }

        public void LogoutUser(string jwt)
        {
            var user = _authenticationRepository.GetUserByJwt(jwt);
            if (user == null) throw new Exception("Wrong credentials");

            _authenticationRepository.LogOutUser(user);
        }
    }
}

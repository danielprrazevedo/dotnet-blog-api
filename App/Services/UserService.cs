using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Models.Interfaces;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogApi.App.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        private IPasswordHasher<IUser> _hasher;
        public UserService(IUserRepository repository, IPasswordHasher<IUser> hasher) : base(repository)
        {
            _hasher = hasher;
        }

        public async Task<IUser> GetUserByUsername(string username)
        {
            var query = await repository.GetAllAsync();
            var result = query.Where(q => q.Username == username).FirstOrDefault();
            return result;
        }

        public async Task<IUser> AuthUser(string username, string password)
        {
            var user = await GetUserByUsername(username);
            if (user == null) return null;
            var result = _hasher.VerifyHashedPassword(user, user.Password, password);
            if (result != PasswordVerificationResult.Failed) return user;
            return null;
        }

        public string GenerateToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public override async Task<User> AddAsync(User obj)
        {
            obj.Password = _hasher.HashPassword(obj, obj.Password);
            return await base.AddAsync(obj);
        }

        public override async Task<User> UpdateAsync(object id, User obj)
        {
            if (obj.Password != "" && obj.Password != null)
            {
                obj.Password = _hasher.HashPassword(obj, obj.Password);
            }
            else
            {
                var entity = await this.GetByIdAsync(id);
                obj.Password = entity.Password;
            }
            return await base.UpdateAsync(id, obj);
        }
    }
}
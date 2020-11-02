using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Models.Interfaces;
using BlogApi.Core.Interfaces;

namespace BlogApi.App.Services.Interfaces
{
    public interface IUserService : IServiceBase<User>
    {
        string GenerateToken(IUser user);

        Task<IUser> GetUserByUsername(string username);
        Task<IUser> AuthUser(string username, string password);
    }
}
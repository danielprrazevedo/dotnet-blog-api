using BlogApi.App.Models;
using BlogApi.App.Models.Interfaces;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.Core.Database;

namespace BlogApi.App.Repositories
{
    public class UserRepository : RepositoryAsync<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        { }
    }
}
using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.Core.Database;

namespace BlogApi.App.Repositories
{
    public class PostRepository : RepositoryAsync<Post>, IPostRepository
    {
        public PostRepository(Context context) : base(context)
        { }
    }
}
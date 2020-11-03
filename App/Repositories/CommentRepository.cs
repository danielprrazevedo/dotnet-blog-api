using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.Core.Database;

namespace BlogApi.App.Repositories
{
    public class CommentRepository : RepositoryAsync<Comment>, ICommentRepository
    {
        public CommentRepository(Context context) : base(context)
        { }
    }
}
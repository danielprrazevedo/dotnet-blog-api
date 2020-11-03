using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;

namespace BlogApi.App.Services
{
    public class CommentService : ServiceBase<Comment>, ICommentService
    {
        public CommentService(ICommentRepository repository) : base(repository)
        { }
    }
}
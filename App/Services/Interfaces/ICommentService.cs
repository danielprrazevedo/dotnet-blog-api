using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.Core.Interfaces;

namespace BlogApi.App.Services.Interfaces
{
    public interface ICommentService : IServiceBase<Comment>
    {
    }
}
using BlogApi.App.Models;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.App.Repositories.Interfaces
{
    public interface IFileRepository : IRepositoryAsync<File>
    { }
}
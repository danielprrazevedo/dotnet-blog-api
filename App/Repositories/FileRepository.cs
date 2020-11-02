using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.Core.Database;

namespace BlogApi.App.Repositories
{
    public class FileRepository : RepositoryAsync<File>, IFileRepository
    {
        public FileRepository(Context context) : base(context)
        { }
    }
}
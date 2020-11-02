using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;

namespace BlogApi.App.Services
{
    public class FileService : ServiceBase<File>, IFileService
    {
        public FileService(IFileRepository repository) : base(repository)
        { }
        public Task<File> Store()
        {
            return GetByIdAsync("");
        }
    }
}
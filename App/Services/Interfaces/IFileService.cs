using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.Core.Interfaces;

namespace BlogApi.App.Services.Interfaces
{
    public interface IFileService : IServiceBase<File>
    {
        Task<File> Store();
    }
}
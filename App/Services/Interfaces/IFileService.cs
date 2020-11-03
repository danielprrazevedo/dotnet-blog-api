using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BlogApi.App.Services.Interfaces
{
    public interface IFileService : IServiceBase<File>
    {
        Task<File> Store(IFormFile file);
    }
}
using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Repositories.Interfaces;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;
using Microsoft.AspNetCore.Http;

namespace BlogApi.App.Services
{
    public class FileService : ServiceBase<File>, IFileService
    {
        public FileService(IFileRepository repository) : base(repository)
        { }
        public async Task<File> Store(IFormFile file)
        {
            File item = new File();
            item.Name = file.FileName;
            item.Size = file.Length;
            item.Type = file.ContentType;

            File result = await AddAsync(item);

            using (var stream = System.IO.File.Create($"files/{result.Id.ToString()}"))
            {
                await file.CopyToAsync(stream);
            }
            return result;
        }
    }
}
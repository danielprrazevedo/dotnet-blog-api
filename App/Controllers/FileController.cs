using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BlogApi.App.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase<File>
    {
        protected new readonly IFileService _service;
        public FileController(IFileService service) : base(service) => _service = service;

        [HttpPost("store")]
        public async Task<IActionResult> Store(List<IFormFile> file)
        {
            if (file.ToArray().Length <= 0)
                throw new ArgumentNullException("file", "file item non passed");

            IFormFile _file = file[0];
            File item = new File();
            item.Name = _file.FileName;
            item.Size = Convert.ToInt32(_file.Length);
            item.Type = _file.ContentType;

            File result = await _service.AddAsync(item);

            using (var stream = System.IO.File.Create($"files/{result.Id.ToString()}"))
            {
                await _file.CopyToAsync(stream);
            }
            result.Url = $"{GetUrl()}/api/file/download/{result.Id}";
            await _service.UpdateAsync(result.Id, result);

            Console.WriteLine(result);
            return Json(result);
        }
        private string GetUrl()
        {
            return $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            Console.WriteLine("passou aqui");
            File result = await _service.GetByIdAsync(id);

            string filename = "files/" + result.Id.ToString();


            var memory = new System.IO.MemoryStream();
            using (var stream = new System.IO.FileStream(filename, System.IO.FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            Response.Headers.Add("Content-Disposition", $"filename={result.Name}");
            return File(memory, result.Type);
        }
    }
}
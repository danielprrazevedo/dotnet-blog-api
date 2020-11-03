using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.App.Models;
using BlogApi.App.Services.Interfaces;
using BlogApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BlogApi.App.Controllers
{
    [Authorize]
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

            var result = await _service.Store(file[0]);

            result.Url = $"{GetUrl()}/api/file/download/{result.Id}";
            await _service.UpdateAsync(result.Id, result);

            return Json(result);
        }
        private string GetUrl()
        {
            return $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        }

        [AllowAnonymous]
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
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
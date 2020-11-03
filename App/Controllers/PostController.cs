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
    [Route("api/post")]
    public class PostController : ControllerBase<Post>
    {
        protected new readonly IPostService _service;
        public PostController(IPostService service) : base(service) => _service = service;
    }
}
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
    [Route("api/comment")]
    public class CommentController : ControllerBase<Comment>
    {
        protected new readonly ICommentService _service;
        public CommentController(ICommentService service) : base(service) => _service = service;
    }
}
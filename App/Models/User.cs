using System.Collections.Generic;
using BlogApi.App.Models.Interfaces;
using BlogApi.Core.Database;

namespace BlogApi.App.Models
{
    public class User : Entity, IUser
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Post> Posts { get; set; }
    }
}
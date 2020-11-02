using System;
using BlogApi.Core.Database;

namespace BlogApi.App.Models
{
    public class Comment : Entity
    {
        public String Text { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
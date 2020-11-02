using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BlogApi.Core.Database;

namespace BlogApi.App.Models
{
    [DataContract(Name = "Post")]
    public class Post : Entity
    {
        public String Title { get; set; }
        public String Text { get; set; }
        public User User { get; set; }
        public File Cover { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
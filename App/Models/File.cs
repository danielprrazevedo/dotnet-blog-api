using System;
using BlogApi.Core.Database;

namespace BlogApi.App.Models
{
    public class File : Entity
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public long Size { get; set; }
        public String Url { get; set; }
    }
}
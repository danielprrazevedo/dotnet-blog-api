using System;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.App.Models.Interfaces
{
    public interface IFile : IEntity
    {
        String Name { get; set; }
        String Type { get; set; }
        int Size { get; set; }
    }
}
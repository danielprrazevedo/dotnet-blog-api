using System;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.App.Models.Interfaces
{
    public interface IComment : IEntity
    {
        String Text { get; set; }
        IPost Post { get; set; }
        IUser User { get; set; }
    }
}
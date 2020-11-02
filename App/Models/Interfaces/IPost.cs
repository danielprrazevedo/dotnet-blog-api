using System;
using System.Collections.Generic;
using BlogApi.Core.Interfaces.Database;

namespace BlogApi.App.Models.Interfaces
{
    public interface IPost : IEntity
    {
        String Title { get; set; }
        String Text { get; set; }
        IUser User { get; set; }
        IFile Cover { get; set; }
        IList<IComment> Comments { get; set; }
    }
}
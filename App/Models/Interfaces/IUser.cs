using BlogApi.Core.Interfaces.Database;

namespace BlogApi.App.Models.Interfaces
{
    public interface IUser : IEntity
    {
        string Name { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
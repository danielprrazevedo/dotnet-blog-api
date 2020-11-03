using BlogApi.App.Repositories;
using BlogApi.App.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApi.App.IoC
{
    public static class RepositoryIoC
    {
        public static void AppRepositoryIoC(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
        }
    }
}
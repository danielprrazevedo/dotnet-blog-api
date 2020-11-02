using BlogApi.App.Services;
using BlogApi.App.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApi.App.IoC
{
    public static class SercvicesIoC
    {
        public static void AppServicesIoC(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
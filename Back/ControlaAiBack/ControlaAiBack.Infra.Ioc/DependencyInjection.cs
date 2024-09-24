using ControlaAiBack.Application.Interfaces;
using ControlaAiBack.Application.Services;
using ControlaAiBack.Domain.Interfaces;
using ControlaAiBack.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ControlaAiBack.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            
        }
    }
}

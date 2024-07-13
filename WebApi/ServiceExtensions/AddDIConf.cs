using DAL.Interfaces;
using DAL.Repository;
using DAL;
using BLL.Interfaces;
using BLL.Services;

namespace WebApi.ServiceExtensions
{
    public static class AddDIConf
    {
        public static IServiceCollection AddDIConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}

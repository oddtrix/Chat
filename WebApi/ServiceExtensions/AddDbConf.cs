using Microsoft.EntityFrameworkCore;
using DAL.Contexts;

namespace WebApi.ServiceExtensions
{
    public static class AddDbConf
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, string domainConnectionString)
        {
            services.AddDbContext<DomainContext>(options =>
            {
                options.UseSqlServer(domainConnectionString);
            });

            return services;
        }
    }
}

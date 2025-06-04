using CDeising.ShareBuildersTest.Application.Common.Interfaces;
using CDeising.ShareBuildersTest.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CDeising.ShareBuildersTest.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add the required infrastructure-level services to the DI container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory>(_ =>
            {
                return new DbConnectionFactory(ConfigurationManager.ConnectionStrings["ShareBuildersTest"].ConnectionString);
            });

            return services;
        }
    }
}

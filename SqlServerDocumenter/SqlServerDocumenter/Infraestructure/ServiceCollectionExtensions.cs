using Microsoft.Extensions.Configuration;
using SqlServerDocumenter;
using SqlServerDocumenter.Infraestructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class to extend services class in method 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the necesary service to DI container
        /// </summary>
        /// <param name="services">IServiceCollection to inject the dependecies</param>
        /// <param name="configuration">configuration to add SqlDocumenterConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerDocumenter(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlDocumenterConfiguration>(configuration.GetSection(nameof(SqlDocumenterConfiguration)))
                .AddScoped<IDocumenter, SqlDocumenter>();
            return services;
        }
    }
}
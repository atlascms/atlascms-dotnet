using Atlas.Core;
using Atlas.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AspNetCore
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds Atlas SDK to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection.</param>
        /// <param name="options">The delegate that will be used to build the configuration.</param>
        /// <returns>The IServiceCollection.</returns>
        public static IServiceCollection AddAtlasSDK(this IServiceCollection services, Action<AtlasOptions> options)
        {
            services.Configure(options);
            services.AddTransient<IAtlasClient>((sp) =>
            {
                return new AtlasClient(sp.GetRequiredService<IOptions<AtlasOptions>>().Value);
            });

            return services;
        }
    }
}

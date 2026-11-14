using JQuants.Net.Abstractions.Interfaces;
using JQuants.Net.Equities;
using JQuants.Net.Infrastructure.Http;
using JQuants.Net.Infrastructure.RateLimiting;
using JQuants.Net.Infrastructure.Time;
using JQuants.Net.Options;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace JQuants.Net.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering J-Quants services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers J-Quants services.
        /// </summary>
        /// <param name="services">
        /// The service collection.
        /// </param>
        /// <param name="configure">
        /// The options configuration delegate.
        /// </param>
        /// <returns>
        /// The service collection.
        /// </returns>
        public static IServiceCollection AddJQuantsNet(
            this IServiceCollection services,
            Action<JQuantsOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new JQuantsOptions();

            configure(options);

            services.AddSingleton(options);

            services.AddSingleton<IClock, SystemClock>();

            services.AddSingleton<IDelayProvider, DelayProvider>();

            services.AddSingleton<IJQuantsRateLimiter>(serviceProvider =>
                new JQuantsRateLimiter(
                    serviceProvider.GetRequiredService<IClock>(),
                    serviceProvider.GetRequiredService<IDelayProvider>(),
                    options.RequestsPerMinute));

            services.AddHttpClient<IJQuantsHttpClient, JQuantsHttpClient>();

            services.AddTransient<IJQuantsRequestExecutor, JQuantsRequestExecutor>();

            services.AddTransient<IJQuantsEquitiesClient, JQuantsEquitiesClient>();

            services.AddTransient<IJQuantsApi, JQuantsApi>();

            return services;
        }

        public static IServiceCollection AddJQuantsNet(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddJQuantsNet((option) => { });
        }
    }
}
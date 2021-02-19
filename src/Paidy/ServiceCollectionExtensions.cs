using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Paidy.Payments;
using Paidy.Tokens;



namespace Paidy
{
    /// <summary>
    /// Provides <see cref="IServiceCollection"/> extension functions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Paidy services to DI.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddPaidy(this IServiceCollection services, PaidyOptions options)
        {
            const string httpClientName = "___Paidy.HttpClient___";
            services.AddHttpClient(httpClientName, client =>
            {
                client.BaseAddress = new(options.ApiEndpoint);
                client.DefaultRequestHeaders.Authorization = new("Bearer", options.SecretKey);
                if (options.ApiVersion is not null)
                    client.DefaultRequestHeaders.Add("Paidy-Version", options.ApiVersion);
            });
            services.TryAddSingleton(static x => new PaymentService(getHttpClient(x)));
            services.TryAddSingleton(static x => new TokenService(getHttpClient(x)));
            return services;

            #region Local functions
            static HttpClient getHttpClient(IServiceProvider provider)
            {
                var factory = provider.GetRequiredService<IHttpClientFactory>();
                return factory.CreateClient(httpClientName);
            }
            #endregion
        }
    }
}

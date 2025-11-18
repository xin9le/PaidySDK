using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Paidy.Internals.Net.Http;
using Paidy.Payments;
using Paidy.Tokens;

namespace Paidy;



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
        => services.AddPaidy(_ => options);


    /// <summary>
    /// Add Paidy services to DI.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsFactory"></param>
    /// <returns></returns>
    public static IServiceCollection AddPaidy(this IServiceCollection services, Func<IServiceProvider, PaidyOptions> optionsFactory)
    {
        services.AddHttpClient(HttpClientNames.Paidy, (provider, client) =>
        {
            var options = optionsFactory(provider);
            client.BaseAddress = new(options.ApiEndpoint);
            client.DefaultRequestHeaders.Authorization = new("Bearer", options.SecretKey);
            if (options.ApiVersion is not null)
                client.DefaultRequestHeaders.Add("Paidy-Version", options.ApiVersion);
        });
        services.TryAddSingleton(static x => new PaymentService(getFactory(x)));
        services.TryAddSingleton(static x => new TokenService(getFactory(x)));
        return services;

        #region Local functions
        static IHttpClientFactory getFactory(IServiceProvider provider)
            => provider.GetRequiredService<IHttpClientFactory>();
        #endregion
    }
}

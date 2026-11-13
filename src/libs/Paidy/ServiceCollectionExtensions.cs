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
    extension(IServiceCollection @this)
    {
        /// <summary>
        /// Add Paidy services to DI.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IServiceCollection AddPaidy(PaidyOptions options)
            => @this.AddPaidy(_ => options);


        /// <summary>
        /// Add Paidy services to DI.
        /// </summary>
        /// <param name="optionsFactory"></param>
        /// <returns></returns>
        public IServiceCollection AddPaidy(Func<IServiceProvider, PaidyOptions> optionsFactory)
        {
            @this.AddHttpClient(HttpClientNames.Paidy, (provider, client) =>
            {
                var options = optionsFactory(provider);
                client.BaseAddress = new(options.ApiEndpoint);
                client.DefaultRequestHeaders.Authorization = new("Bearer", options.SecretKey);
                if (options.ApiVersion is not null)
                    client.DefaultRequestHeaders.Add("Paidy-Version", options.ApiVersion);
            });
            @this.TryAddSingleton(static x => new PaymentService(getFactory(x)));
            @this.TryAddSingleton(static x => new TokenService(getFactory(x)));
            return @this;

            #region Local functions
            static IHttpClientFactory getFactory(IServiceProvider provider)
                => provider.GetRequiredService<IHttpClientFactory>();
            #endregion
        }
    }
}

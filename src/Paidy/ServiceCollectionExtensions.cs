using Microsoft.Extensions.DependencyInjection;



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
                client.DefaultRequestHeaders.Add("Paidy-Version", options.ApiVersion);
            });
            return services;
        }
    }
}

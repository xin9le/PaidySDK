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
            return services;
        }
    }
}

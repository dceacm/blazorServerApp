using BlazorServerApp.OpenAPIs;
using Microsoft.Extensions.Options;

namespace BlazorServerApp;

public static class RegisterExtensions
{
    /// <summary>
    /// Añade la misma configuración a los clientes http mediante IHttpClientFactory
    /// </summary>
    /// <typeparam name="TInterface"></typeparam>
    /// <typeparam name="TClient"></typeparam>
    /// <param name="services"></param>
    /// <returns>Colección de servicios</returns>
    public static IServiceCollection AddCustomHttpClient<TInterface, TClient>(this IServiceCollection services)
        where TClient : class, TInterface
        where TInterface : class
    {
        services.AddOptions<ApiClientOptions>()
                    .BindConfiguration(ApiClientOptions.SectionName)
                    .ValidateOnStart();

        services.AddHttpClient<TInterface, TClient>()
            // Cualquier conexión http dentro del pool de conexiones, se cerrará pasados 5 minutos.
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                };
            })
            // Por defecto, IHttpClientFactory recicla los manejadores cada 2 min. Como estamos asegurando
            // que las conexiones se van a cerrar pasados máximo 5 min, evitamos el reciclado automático y
            // periódico cada 2 min.
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }

    /// <summary>
    /// Agrega configuración al cliente http desde el que se llama.
    /// </summary>
    /// <param name="httpClient"></param>
    /// <param name="options"></param>
    /// <returns>Cliente http con configuración añadida</returns>
    public static HttpClient AddHttpConfig(this HttpClient httpClient, IOptions<ApiClientOptions> options)
    {
        httpClient.Timeout = new TimeSpan(0, 0, options.Value.TiempoMaximoEsperaEnSegundos);

        return httpClient;
    }
}
using Microsoft.Extensions.Options;

namespace BlazorServerApp.OpenAPIs
{
    public partial class ApiClientCustom : ApiClient
    {
        public ApiClientCustom(
            IOptions<ApiClientOptions> options
            , HttpClient httpClient)
            : base(
                baseUrl: options.Value.BaseUrl,
                httpClient: httpClient.AddHttpConfig(options))
        {
        }
    }
}
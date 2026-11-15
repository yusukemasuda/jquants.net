using JQuants.Net.Options;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Http
{
    internal sealed class JQuantsHttpClient
        : IJQuantsHttpClient
    {
        private readonly HttpClient httpClient;

        public JQuantsHttpClient(
            HttpClient httpClient,
            JQuantsOptions options)
        {
            this.httpClient = httpClient
                ?? throw new ArgumentNullException(nameof(httpClient));

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            httpClient.BaseAddress =
                new Uri("https://api.jquants.com");

            httpClient.DefaultRequestHeaders.Add(
                "x-api-key",
                options.ApiKey);
        }

        public Task<HttpResponseMessage> GetAsync(
            string requestUri,
            CancellationToken cancellationToken)
        {
            var res = httpClient.GetAsync(
                requestUri,
                cancellationToken);

            return res;
        }
    }
}
using JQuants.Net.Infrastructure.RateLimiting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Http
{
    internal sealed class JQuantsRequestExecutor
        : IJQuantsRequestExecutor
    {
        private readonly IJQuantsHttpClient httpClient;
        private readonly IJQuantsRateLimiter rateLimiter;

        public JQuantsRequestExecutor(
            IJQuantsHttpClient httpClient,
            IJQuantsRateLimiter rateLimiter)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.rateLimiter = rateLimiter ?? throw new ArgumentNullException(nameof(rateLimiter));
        }

        public async Task<TResponse> GetAsync<TResponse>(
            string requestUri,
            CancellationToken cancellationToken)
        {
            await rateLimiter.WaitAsync(cancellationToken).ConfigureAwait(false);

            using var response = await httpClient.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<TResponse>(content);

            return result ?? throw new InvalidOperationException("The response body could not be deserialized.");
        }
    }
}

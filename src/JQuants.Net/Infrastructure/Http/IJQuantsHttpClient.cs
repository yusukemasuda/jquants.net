using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Http
{
    internal interface IJQuantsHttpClient
    {
        Task<HttpResponseMessage> GetAsync(
            string requestUri,
            CancellationToken cancellationToken);
    }
}
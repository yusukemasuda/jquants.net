using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Http
{
    internal interface IJQuantsRequestExecutor
    {
        Task<TResponse> GetAsync<TResponse>(
            string requestUri,
            CancellationToken cancellationToken);
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.RateLimiting
{
    internal interface IJQuantsRateLimiter
    {
        Task WaitAsync(
            CancellationToken cancellationToken);
    }
}

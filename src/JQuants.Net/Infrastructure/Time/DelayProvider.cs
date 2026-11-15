using System;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Time
{
    internal sealed class DelayProvider
        : IDelayProvider
    {
        public Task DelayAsync(
            TimeSpan delay,
            CancellationToken cancellationToken)
        {
            return Task.Delay(delay, cancellationToken);
        }
    }

}

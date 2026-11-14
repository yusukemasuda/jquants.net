using JQuants.Net.Infrastructure.Time;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.RateLimiting
{
    internal sealed class JQuantsRateLimiter
        : IJQuantsRateLimiter
    {
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(1);

        private readonly IClock clock;
        private readonly IDelayProvider delayProvider;
        private readonly int requestsPerMinute;

        private readonly SemaphoreSlim semaphore;

        private readonly Queue<DateTime> requestHistory;

        public JQuantsRateLimiter(
            IClock clock,
            IDelayProvider delayProvider,
            int requestsPerMinute)
        {
            if (requestsPerMinute <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(requestsPerMinute), "Requests per minute must be a positive integer.");
            }

            this.clock = clock ?? throw new ArgumentNullException(nameof(clock));
            this.delayProvider = delayProvider ?? throw new ArgumentNullException(nameof(delayProvider));
            this.requestsPerMinute = requestsPerMinute;
            
            this.semaphore = new SemaphoreSlim(1, 1);
            this.requestHistory = new Queue<DateTime>();
        }

        public async Task WaitAsync(
            CancellationToken cancellationToken)
        {
            while (true)
            {
                TimeSpan waitTime;

                await semaphore.WaitAsync(cancellationToken);

                try
                {
                    var now = clock.UtcNow;

                    RemoveExpiredRequests(now);

                    if (requestHistory.Count < requestsPerMinute)
                    {
                        requestHistory.Enqueue(now);
                        return;
                    }

                    var oldestRequest = requestHistory.Peek();

                    waitTime = oldestRequest + Window - now;
                }
                finally
                {
                    semaphore.Release();
                }

                if (waitTime <= TimeSpan.Zero)
                {
                    continue;
                }

                await delayProvider.DelayAsync(
                    waitTime,
                    cancellationToken);
            }
        }

        private void RemoveExpiredRequests(DateTime now)
        {
            while (requestHistory.Count > 0)
            {
                var oldestRequest =
                    requestHistory.Peek();

                if (now - oldestRequest < Window)
                {
                    break;
                }

                requestHistory.Dequeue();
            }
        }
    }
}
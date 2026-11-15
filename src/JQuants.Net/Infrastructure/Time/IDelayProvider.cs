using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JQuants.Net.Infrastructure.Time
{
    internal interface IDelayProvider
    {
        Task DelayAsync(
            TimeSpan delay,
            CancellationToken cancellationToken);
    }
}

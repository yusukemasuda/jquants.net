using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using JQuants.Net.Infrastructure.Time;

namespace JQuants.Net.UnitTests.TestDoubles
{
    internal sealed class FakeDelayProvider
        : IDelayProvider
    {
        private readonly FakeClock clock;

        public FakeDelayProvider(FakeClock clock)
        {
            this.clock = clock;
        }

        public IList<TimeSpan> Delays { get; }
            = new List<TimeSpan>();

        public Task DelayAsync(
            TimeSpan delay,
            CancellationToken cancellationToken)
        {
            Delays.Add(delay);
            clock.Advance(delay);
            return Task.CompletedTask;
        }
    }
}
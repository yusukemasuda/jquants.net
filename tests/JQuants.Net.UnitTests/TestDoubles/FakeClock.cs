using JQuants.Net.Infrastructure.Time;

namespace JQuants.Net.UnitTests.TestDoubles
{
    internal sealed class FakeClock : IClock
    {
        public DateTime UtcNow { get; private set; }

        public FakeClock(DateTime utcNow)
        {
            UtcNow = utcNow;
        }

        public void Advance(TimeSpan timeSpan)
        {
            UtcNow = UtcNow.Add(timeSpan);
        }

        public void Set(DateTime utcNow)
        {
            UtcNow = utcNow;
        }
    }
}

using System;

namespace JQuants.Net.Infrastructure.Time
{
    internal sealed class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

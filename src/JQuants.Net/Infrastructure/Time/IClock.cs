using System;
using System.Collections.Generic;
using System.Text;

namespace JQuants.Net.Infrastructure.Time
{
    internal interface IClock
    {
        DateTime UtcNow { get; }
    }
}

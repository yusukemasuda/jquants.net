using JQuants.Net.Abstractions.Interfaces;
using System;

namespace JQuants.Net
{
    internal sealed class JQuantsApi : IJQuantsApi
    {
        public JQuantsApi(
            IJQuantsEquitiesClient equities)
        {
            Equities = equities ?? throw new ArgumentNullException(nameof(equities));
        }

        public IJQuantsEquitiesClient Equities { get; }
    }
}
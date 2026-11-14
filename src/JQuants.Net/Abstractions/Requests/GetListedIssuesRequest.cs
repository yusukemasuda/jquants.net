using System;

namespace JQuants.Net.Abstractions.Requests
{
    /// <summary>
    /// Represents a request for the equities master endpoint.
    /// </summary>
    public sealed class GetListedIssuesRequest
    {
        /// <summary>
        /// Gets or sets the issue code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the effective date.
        /// </summary>
        public DateTime? Date { get; set; }
    }
}
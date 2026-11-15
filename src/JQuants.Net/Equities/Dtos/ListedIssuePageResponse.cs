using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JQuants.Net.Equities.Dtos
{
    /// <summary>
    /// Represents a paged response returned by the equities master endpoint.
    /// </summary>
    internal sealed class ListedIssuePageResponse
    {
        /// <summary>
        /// Gets or sets the listed issues contained in the current page.
        /// </summary>
        [JsonPropertyName("data")]
        public IList<ListedIssueResponse> Data { get; set; } = new List<ListedIssueResponse>();

        /// <summary>
        /// Gets or sets the pagination key used to retrieve the next page.
        /// </summary>
        [JsonPropertyName("pagination_key")]
        public string PaginationKey { get; set; } = string.Empty;
    }
}
namespace JQuants.Net.Options
{
    /// <summary>
    /// Represents configuration options for the J-Quants API.
    /// </summary>
    public sealed class JQuantsOptions
    {
        /// <summary>
        /// Gets or sets the API key used to authenticate requests.
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the maximum number of requests allowed per minute.
        /// </summary>
        public int RequestsPerMinute { get; set; } = 60;
    }
}
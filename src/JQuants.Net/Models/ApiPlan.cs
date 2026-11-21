using System;
using System.Collections.Generic;
using System.Text;

namespace JQuants.Net.Models
{
    public sealed class ApiPlan
    {
        /// <summary>
        /// Represents a J-Quants API subscription plan.
        ///
        /// An API plan defines the API key and the rate limits
        /// applied when accessing the J-Quants API.
        /// </summary>
        private ApiPlan(
            string apiKey,
            string name,
            int requestsPerMinute)
        {
            ApiKey = apiKey;
            Name = name;
            RequestsPerMinute = requestsPerMinute;
        }


        /// <summary>
        /// Gets or sets the API key used to authenticate requests.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Gets or sets the name of this plan
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the maximum number of requests allowed per minute.
        /// </summary>
        public int RequestsPerMinute { get; }

        internal static ApiPlan Create(
            string apiKey,
            string name,
            int requestsPerMinute)
        {
            return new ApiPlan(
                apiKey,
                name,
                requestsPerMinute);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace JQuants.Net.Models
{
    /// <summary>
    /// Provides factory methods for creating predefined J-Quants API plans.
    ///
    /// The generated <see cref="ApiPlan"/> instances include the rate limits
    /// associated with each supported J-Quants subscription plan.
    ///
    /// Available plans:
    /// - Free: 5 requests per minute
    /// - Light: 60 requests per minute
    /// - Standard: 120 requests per minute
    /// - Premium: 500 requests per minute
    /// </summary>
    public static class ApiPlans
    {
        /// <summary>
        /// Creates an API plan for the Free subscription tier.
        ///
        /// Rate limit:
        /// - 5 requests per minute
        /// </summary>
        /// <param name="apiKey">
        /// The J-Quants API key.
        /// </param>
        /// <returns>
        /// An <see cref="ApiPlan"/> configured for the Free plan.
        /// </returns>
        public static ApiPlan Free(string apiKey)
        {
            return ApiPlan.Create(apiKey, "Free", 5);
        }

        /// <summary>
        /// Creates an API plan for the Light subscription tier.
        ///
        /// Rate limit:
        /// - 60 requests per minute
        /// </summary>
        /// <param name="apiKey">
        /// The J-Quants API key.
        /// </param>
        /// <returns>
        /// An <see cref="ApiPlan"/> configured for the Free plan.
        /// </returns>
        public static ApiPlan Light(string apiKey)
        {
            return ApiPlan.Create(apiKey, "Light", 60);
        }

        /// <summary>
        /// Creates an API plan for the Standard subscription tier.
        ///
        /// Rate limit:
        /// - 120 requests per minute
        /// </summary>
        /// <param name="apiKey">
        /// The J-Quants API key.
        /// </param>
        /// <returns>
        /// An <see cref="ApiPlan"/> configured for the Free plan.
        /// </returns>
        public static ApiPlan Standard(string apiKey)
        {
            return ApiPlan.Create(apiKey, "Standard", 120);
        }

        /// <summary>
        /// Creates an API plan for the Premium subscription tier.
        ///
        /// Rate limit:
        /// - 500 requests per minute
        /// </summary>
        /// <param name="apiKey">
        /// The J-Quants API key.
        /// </param>
        /// <returns>
        /// An <see cref="ApiPlan"/> configured for the Free plan.
        /// </returns>
        public static ApiPlan Premium(string apiKey)
        {
            return ApiPlan.Create(apiKey, "Premium", 500);
        }
    }
}

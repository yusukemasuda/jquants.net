using System;

namespace JQuants.Net.Abstractions.Models
{
    /// <summary>
    /// Represents a listed issue returned by the J-Quants equities master endpoint.
    /// </summary>
    public sealed class ListedIssue
    {
        /// <summary>
        /// Gets or sets the effective date of the issue information.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the issue code.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string CoName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company name in English.
        /// </summary>
        public string CoNameEn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the 17-sector code.
        /// </summary>
        public string S17 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the 17-sector name.
        /// </summary>
        public string S17Nm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the 33-sector code.
        /// </summary>
        public string S33 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the 33-sector name.
        /// </summary>
        public string S33Nm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the scale category code.
        /// </summary>
        public string ScaleCat { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the market category code.
        /// </summary>
        public string Mkt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the market category name.
        /// </summary>
        public string MktNm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the margin category code.
        /// </summary>
        public string Mrgn { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the margin category name.
        /// </summary>
        public string MrgnNm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category code.
        /// </summary>
        public string ProdCat { get; set; } = string.Empty;
    }
}
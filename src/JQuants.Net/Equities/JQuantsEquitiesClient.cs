using JQuants.Net.Abstractions.Interfaces;
using JQuants.Net.Abstractions.Models;
using JQuants.Net.Abstractions.Requests;
using JQuants.Net.Equities.Dtos;
using JQuants.Net.Equities.Mappers;
using JQuants.Net.Infrastructure.Http;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;

namespace JQuants.Net.Equities
{
    internal sealed class JQuantsEquitiesClient
        : IJQuantsEquitiesClient
    {
        private readonly IJQuantsRequestExecutor requestExecutor;

        public JQuantsEquitiesClient(IJQuantsRequestExecutor requestExecutor)
        {
            this.requestExecutor = requestExecutor ?? throw new ArgumentNullException(nameof(requestExecutor));
        }

        public async IAsyncEnumerable<ListedIssue> GetListedIssuesAsync(
                GetListedIssuesRequest request,
                [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string paginationKey = null;

            do
            {
                var requestUri = BuildRequestUri(request, paginationKey);
                var response = await requestExecutor.GetAsync<ListedIssuePageResponse>(requestUri, cancellationToken);
                foreach (var item in response.Data)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return ListedIssueMapper.Map(item);
                }

                paginationKey = response.PaginationKey;
            }
            while (!string.IsNullOrEmpty(paginationKey));
        
        }

        private static string BuildRequestUri(GetListedIssuesRequest request, string paginationKey)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            if (!string.IsNullOrWhiteSpace(request.Code))
            {
                query["code"] = request.Code;
            }

            if (request.Date.HasValue)
            {
                query["date"] = request.Date.Value.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrWhiteSpace(paginationKey))
            {
                query["pagination_key"] = paginationKey;
            }

            return query.Count == 0
                ? "/v2/equities/master"
                : "/v2/equities/master?" + query.ToString();
        }
    }
}
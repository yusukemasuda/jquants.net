using JQuants.Net.Abstractions.Models;
using JQuants.Net.Abstractions.Requests;
using System.Collections.Generic;
using System.Threading;

namespace JQuants.Net.Abstractions.Interfaces
{
    public interface IJQuantsEquitiesClient
    {
        IAsyncEnumerable<ListedIssue> GetListedIssuesAsync(
                GetListedIssuesRequest request,
                CancellationToken cancellationToken);
    }
}
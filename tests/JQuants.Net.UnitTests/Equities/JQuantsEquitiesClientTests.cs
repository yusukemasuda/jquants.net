using JQuants.Net.Abstractions.Models;
using JQuants.Net.Abstractions.Requests;
using JQuants.Net.Equities;
using JQuants.Net.Equities.Dtos;
using JQuants.Net.Infrastructure.Http;
using Moq;

namespace JQuants.Net.UnitTests.Equities
{
    internal sealed class JQuantsEquitiesClientTests
    {
        [Test]
        public void Constructor_ShouldThrow_WhenRequestExecutorIsNull()
        {
            Assert.That(
                () => new JQuantsEquitiesClient(null),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("requestExecutor"));
        }

        [Test]
        public async Task GetListedIssuesAsync_ShouldThrow_WhenRequestIsNull()
        {
            var requestExecutor = new Mock<IJQuantsRequestExecutor>();
            var client = new JQuantsEquitiesClient(requestExecutor.Object);

            Assert.That(
                async () => await CollectAsync(
                    client.GetListedIssuesAsync(null, CancellationToken.None)),
                Throws.ArgumentNullException.With.Property("ParamName").EqualTo("request"));
        }

        [Test]
        public async Task GetListedIssuesAsync_ShouldMapResponseAndBuildQuery()
        {
            var requestExecutor = new Mock<IJQuantsRequestExecutor>();
            var response = new ListedIssuePageResponse
            {
                Data =
                [
                    new ListedIssueResponse
                    {
                        Date = "2026-01-02",
                        Code = "1234",
                        CoName = "Example Co.",
                        CoNameEn = "Example Co., Ltd.",
                        S17 = "1",
                        S17Nm = "Food",
                        S33 = "0050",
                        S33Nm = "Foods",
                        ScaleCat = "3",
                        Mkt = "0111",
                        MktNm = "Prime",
                        Mrgn = "1",
                        MrgnNm = "信用",
                        ProdCat = "1"
                    }
                ]
            };

            requestExecutor
                .Setup(x => x.GetAsync<ListedIssuePageResponse>(
                    "/v2/equities/master?code=1234&date=2026-01-02",
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var client = new JQuantsEquitiesClient(requestExecutor.Object);
            var result = await CollectAsync(
                client.GetListedIssuesAsync(
                    new GetListedIssuesRequest
                    {
                        Code = "1234",
                        Date = new DateTime(2026, 1, 2)
                    },
                    CancellationToken.None));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result[0].Date, Is.EqualTo(new DateTime(2026, 1, 2)));
                Assert.That(result[0].Code, Is.EqualTo("1234"));
                Assert.That(result[0].CoNameEn, Is.EqualTo("Example Co., Ltd."));
            }

            requestExecutor.Verify(x => x.GetAsync<ListedIssuePageResponse>(
                "/v2/equities/master?code=1234&date=2026-01-02",
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetListedIssuesAsync_ShouldRequestAllPages()
        {
            var requestExecutor = new Mock<IJQuantsRequestExecutor>();
            requestExecutor
                .SetupSequence(x => x.GetAsync<ListedIssuePageResponse>(
                    It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListedIssuePageResponse
                {
                    Data =
                    [
                        new ListedIssueResponse { Date = "2026-01-01", Code = "1111" }
                    ],
                    PaginationKey = "next-page"
                })
                .ReturnsAsync(new ListedIssuePageResponse
                {
                    Data =
                    [
                        new ListedIssueResponse { Date = "2026-01-02", Code = "2222" }
                    ]
                });

            var client = new JQuantsEquitiesClient(requestExecutor.Object);
            var result = await CollectAsync(
                client.GetListedIssuesAsync(
                    new GetListedIssuesRequest(),
                    CancellationToken.None));

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result[0].Code, Is.EqualTo("1111"));
                Assert.That(result[1].Code, Is.EqualTo("2222"));
            });

            requestExecutor.Verify(x => x.GetAsync<ListedIssuePageResponse>(
                "/v2/equities/master", It.IsAny<CancellationToken>()), Times.Once);
            requestExecutor.Verify(x => x.GetAsync<ListedIssuePageResponse>(
                "/v2/equities/master?pagination_key=next-page",
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetListedIssuesAsync_ShouldPassCancellationToken()
        {
            var requestExecutor = new Mock<IJQuantsRequestExecutor>();
            requestExecutor
                .Setup(x => x.GetAsync<ListedIssuePageResponse>(
                    It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListedIssuePageResponse
                {
                    PaginationKey = string.Empty,
                });

            var client = new JQuantsEquitiesClient(requestExecutor.Object);
            using var cancellationTokenSource = new CancellationTokenSource();

            await CollectAsync(
                client.GetListedIssuesAsync(
                    new GetListedIssuesRequest(),
                    cancellationTokenSource.Token));

            requestExecutor.Verify(x => x.GetAsync<ListedIssuePageResponse>(
                It.IsAny<string>(),
                cancellationTokenSource.Token),
                Times.Once);
        }

        [Test]
        public void GetListedIssuesAsync_ShouldHonorCancellation()
        {
            var requestExecutor = new Mock<IJQuantsRequestExecutor>();
            requestExecutor
                .Setup(x => x.GetAsync<ListedIssuePageResponse>(
                    It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ListedIssuePageResponse
                {
                    Data =
                    [
                        new ListedIssueResponse { Date = "2026-01-01", Code = "1234" }
                    ]
                });

            var client = new JQuantsEquitiesClient(requestExecutor.Object);
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            Assert.That(
                async () => await CollectAsync(
                    client.GetListedIssuesAsync(
                        new GetListedIssuesRequest(),
                        cancellationTokenSource.Token)),
                Throws.TypeOf<OperationCanceledException>());
        }

        private static async Task<List<ListedIssue>> CollectAsync(
            IAsyncEnumerable<ListedIssue> issues)
        {
            var result = new List<ListedIssue>();

            await foreach (var issue in issues)
            {
                result.Add(issue);
            }

            return result;
        }
    }
}

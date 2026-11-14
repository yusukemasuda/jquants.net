using JQuants.Net.Infrastructure.Http;
using JQuants.Net.Infrastructure.RateLimiting;
using Moq;
using System.Net;
using System.Text;

namespace JQuants.Net.UnitTests.Infrastructure.Http
{
    internal sealed class JQuantsRequestExecutorTests
    {
        [Test]
        public async Task ShouldDeserializeResponse_WhenRequestSucceeds()
        {
            // Arrange
            var responseBody = "{ \"Value\": \"Test\" }";
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
            };

            var httpClient = new Mock<IJQuantsHttpClient>();
            httpClient.Setup(x => x.GetAsync("/test", It.IsAny<CancellationToken>()))
                    .ReturnsAsync(httpResponse);

            var rateLimiter = new Mock<IJQuantsRateLimiter>();
            var executor = new JQuantsRequestExecutor(httpClient.Object, rateLimiter.Object);

            // Act
            var result = await executor.GetAsync<TestResponse>("/test", CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Test"));
        }

        [Test]
        public async Task ShouldInvokeRateLimiterBeforeSendingRequest()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}")
            };

            // Arrange
            var httpClient = new Mock<IJQuantsHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(httpResponse);

            var rateLimiter = new Mock<IJQuantsRateLimiter>();
            var executor = new JQuantsRequestExecutor(httpClient.Object, rateLimiter.Object);

            // Act
            await executor.GetAsync<object>("/test", CancellationToken.None);

            // Assert
            rateLimiter.Verify(x => x.WaitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private sealed class TestResponse
        {
            public string? Value { get; set; }
        }
    }
}

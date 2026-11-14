using JQuants.Net.Infrastructure.RateLimiting;
using JQuants.Net.UnitTests.TestDoubles;

namespace JQuants.Net.UnitTests.Infrastructure.RateLimiting
{
    public sealed class JQuantsRateLimiterTests
    {
        [Test]
        public async Task ShouldAllowRequest_WhenNoRequestExists()
        {
            // Arrange

            var clock = new FakeClock(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            var delayProvider = new FakeDelayProvider(clock);

            var rateLimiter = new JQuantsRateLimiter(
                                    clock: clock,
                                    delayProvider: delayProvider,
                                    requestsPerMinute: 60);

            // Act
            await rateLimiter.WaitAsync(CancellationToken.None);

            // Assert
            Assert.That(delayProvider.Delays, Is.Empty);
        }

        [Test]
        public async Task ShouldDelayRequest_WhenLimitExceeded()
        {
            // Arrange

            var clock = new FakeClock(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            var delayProvider = new FakeDelayProvider(clock);

            var rateLimiter = new JQuantsRateLimiter(
                                    clock: clock,
                                    delayProvider: delayProvider,
                                    requestsPerMinute: 2);

            // Act
            await rateLimiter.WaitAsync(CancellationToken.None);
            await rateLimiter.WaitAsync(CancellationToken.None);
            await rateLimiter.WaitAsync(CancellationToken.None);

            // Assert
            Assert.That(delayProvider.Delays.Count, Is.EqualTo(1));
            Assert.That(delayProvider.Delays[0], Is.EqualTo(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public async Task ShouldAllowRequest_WhenOldRequestsExpire()
        {
            // Arrange
            var clock = new FakeClock(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            var delayProvider = new FakeDelayProvider(clock);

            var rateLimiter = new JQuantsRateLimiter(
                                    clock: clock,
                                    delayProvider: delayProvider,
                                    requestsPerMinute: 2);

            await rateLimiter.WaitAsync(CancellationToken.None);
            await rateLimiter.WaitAsync(CancellationToken.None);

            clock.Advance(
                TimeSpan.FromMinutes(1)
                + TimeSpan.FromSeconds(1));

            // Act
            await rateLimiter.WaitAsync(CancellationToken.None);

            // Assert
            Assert.That(delayProvider.Delays, Is.Empty);
        }

        [Test]
        public async Task ShouldCalculateRemainingWaitTimeCorrectly()
        {
            // Arrange
            var clock = new FakeClock(new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            var delayProvider = new FakeDelayProvider(clock);
            var rateLimiter = new JQuantsRateLimiter(
                                    clock: clock,
                                    delayProvider: delayProvider,
                                    requestsPerMinute: 2);

            await rateLimiter.WaitAsync(CancellationToken.None);
            await rateLimiter.WaitAsync(CancellationToken.None);

            clock.Advance(TimeSpan.FromSeconds(30));

            // Act
            await rateLimiter.WaitAsync(CancellationToken.None);

            // Assert
            Assert.That(delayProvider.Delays.Count, Is.EqualTo(1));
            Assert.That(delayProvider.Delays[0], Is.EqualTo(TimeSpan.FromSeconds(30)));
        }
    }
}

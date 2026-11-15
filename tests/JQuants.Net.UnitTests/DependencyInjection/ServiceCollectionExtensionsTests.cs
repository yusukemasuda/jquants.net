using JQuants.Net.Abstractions.Interfaces;
using JQuants.Net.DependencyInjection;
using JQuants.Net.Infrastructure.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JQuants.Net.UnitTests.DependencyInjection
{
    internal sealed class ServiceCollectionExtensionsTests
    {
        [Test]
        public void ShouldRegisterIJQuantsApi()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddJQuantsNet(options =>
            {
                options.ApiKey = "test-api-key";
            });

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var api = serviceProvider.GetService<IJQuantsApi>();

            // Assert
            Assert.That(api, Is.Not.Null);
        }

        [Test]
        public void ShouldRegisterIJQuantsEquitiesClient()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddJQuantsNet(options =>
            {
                options.ApiKey = "test-api-key";
            });

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var client = serviceProvider.GetService<IJQuantsEquitiesClient>();

            // Assert
            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void ShouldRegisterIJQuantsRequestExecutor()
        {
            // Arrange
            var services = new ServiceCollection();

            services.AddJQuantsNet(options =>
            {
                options.ApiKey = "test-api-key";
            });

            var serviceProvider = services.BuildServiceProvider();

            // Act
            var executor = serviceProvider.GetService<IJQuantsRequestExecutor>();

            // Assert
            Assert.That(executor, Is.Not.Null);
        }

        [Test]
        public void AddJQuantsNet_ShouldThrow_WhenServicesIsNull()
        {
            // Arrange
            IServiceCollection services = null!;

            // Act & Assert
            Assert.That(
                () => services.AddJQuantsNet(options => { }),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("services"));
        }

        [Test]
        public void AddJQuantsNet_ShouldThrow_WhenConfigureIsNull()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act & Assert
            Assert.That(
                () => services.AddJQuantsNet(null),
                Throws.ArgumentNullException.With.Property(nameof(ArgumentNullException.ParamName)).EqualTo("configure"));
        }
    }
}

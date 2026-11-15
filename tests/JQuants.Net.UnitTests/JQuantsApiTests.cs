using JQuants.Net.Abstractions;
using JQuants.Net.Abstractions.Interfaces;
using Moq;

namespace JQuants.Net.UnitTests
{
    internal sealed class JQuantsApiTests
    {
        [Test]
        public void Constructor_ShouldThrow_WhenEquitiesIsNull()
        {
            Assert.That(
                () => new JQuantsApi(null),
                Throws.ArgumentNullException
                    .With.Property("ParamName")
                    .EqualTo("equities"));
        }

        [Test]
        public void Constructor_ShouldSetEquities()
        {
            var equities = new Mock<IJQuantsEquitiesClient>();

            var api = new JQuantsApi(equities.Object);

            Assert.That(
                api.Equities,
                Is.SameAs(
                    equities.Object));
        }
    }
}
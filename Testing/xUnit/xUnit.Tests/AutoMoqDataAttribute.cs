using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace xUnit.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(FixtureFactory) { }

        private static IFixture FixtureFactory()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true});
            return fixture;
        }
    }
}
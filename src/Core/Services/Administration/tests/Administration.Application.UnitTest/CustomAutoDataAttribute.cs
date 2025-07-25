using AutoFixture;
using AutoFixture.Xunit2;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;

namespace Administration.Application.UnitTest
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute()
            : base(() => CreateFixture(null))
        {
        }

        public CustomAutoDataAttribute(Action<IFixture> configureFixture)
            : base(() => CreateFixture(configureFixture))
        {
        }

        private static Fixture CreateFixture(Action<IFixture>? configure)
        {
            var fixture = new Fixture();

            RegisterCustomTypes(fixture);
            configure?.Invoke(fixture);

            return fixture;
        }

        private static void RegisterCustomTypes(IFixture fixture)
        {
            fixture.Register(() => ProductName.Parse(fixture.Create<string>()).Value);
            fixture.Register(() => PreparationTime.Parse(fixture.Create<TimeSpan>()).Value);
        }
    }
}

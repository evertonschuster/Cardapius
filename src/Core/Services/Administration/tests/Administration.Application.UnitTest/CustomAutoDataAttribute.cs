using AutoFixture;
using AutoFixture.Xunit2;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;

namespace Administration.Application.UnitTest
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAutoDataAttribute"/> class with default fixture customization for use in unit tests.
        /// </summary>
        public CustomAutoDataAttribute()
            : base(() => CreateFixture(null))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAutoDataAttribute"/> class with additional fixture configuration.
        /// </summary>
        /// <param name="configureFixture">An action to further configure the generated <see cref="IFixture"/> instance.</param>
        public CustomAutoDataAttribute(Action<IFixture> configureFixture)
            : base(() => CreateFixture(configureFixture))
        {
        }

        /// <summary>
        /// Creates and configures a new <see cref="Fixture"/> instance with custom specimen builders and optional additional configuration.
        /// </summary>
        /// <param name="configure">An optional action to further configure the fixture after custom types are registered.</param>
        /// <returns>A configured <see cref="Fixture"/> instance ready for use in test data generation.</returns>
        private static Fixture CreateFixture(Action<IFixture>? configure)
        {
            var fixture = new Fixture();

            RegisterCustomTypes(fixture);
            configure?.Invoke(fixture);

            return fixture;
        }

        /// <summary>
        /// Registers custom specimen builders on the fixture for generating <c>ProductName</c> and <c>PreparationTime</c> value objects.
        /// </summary>
        /// <param name="fixture">The fixture to configure with custom type registrations.</param>
        private static void RegisterCustomTypes(IFixture fixture)
        {
            fixture.Register(() => ProductName.Parse(fixture.Create<string>()).Value);
            fixture.Register(() => PersonName.Parse(PersonName.Empty).Value);
            fixture.Register(() => Phone.Parse(Phone.Empty).Value);
            fixture.Register(() => Email.Parse(Email.Empty).Value);
            fixture.Register(() => PreparationTime.Parse(fixture.Create<TimeSpan>()).Value);
        }
    }
}

using BuildingBlock.Domain.ValueObjects.PersonNames;
using BuildingBlock.Domain.ValueObjects.PersonNames.Exceptions;

namespace BuildingBlock.Domain.UnitTest.ValueObjects;

public class PersonNameTests
{
    [Fact]
    public void Parse_ValidName_ReturnsPersonName()
    {
        var name = "João da Silva";
        var personName = PersonName.Parse(name);

        Assert.Equal(name, personName.Value);
        Assert.True(personName.Validate().IsValid);
    }

    [Fact]
    public void Parse_InvalidName_ThrowsException()
    {
        var invalidName = "";

        Assert.Throws<InvalidPersonNameException>(() => PersonName.Parse(invalidName));
    }

    [Fact]
    public void Empty_ReturnsDefaultName()
    {
        Assert.Equal("Fulano de Tal", PersonName.Empty);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var name = "Maria Oliveira";
        var personName = PersonName.Parse(name);

        Assert.Equal(name, personName.ToString());
    }

    [Fact]
    public void IsValid_ReturnsValidationResult()
    {
        var name = "Carlos Souza";
        var personName = PersonName.Parse(name);

        var result = personName.Validate();

        Assert.True(result.IsValid);
        Assert.Equal(name, result.Value);
    }
}

using Administration.Application.Ncms.Commands.CreateNcm;
using Administration.Domain.Ncms.ValueObjects;
using BuildingBlock.Application;

namespace Administration.Application.UnitTest.Ncms.Commands;

public class CreateNcmHandlerTests
{
    [Fact]
    public async Task Handle_ShouldPersistNcm()
    {
        var repository = Substitute.For<INcmRepository>();
        var uow = Substitute.For<IUnitOfWork>();
        var handler = new CreateNcmHandler(repository, uow);
        var command = new CreateNcmCommand
        {
            Description = "Pizza congelada",
            Code = new NcmCode("19059090"),
            TaxationProfile = new TaxationProfile("Alimentos", 1.25m),
            TaxEstimation = new TaxEstimation(1.5m, 2.5m, 3.5m, 4.5m),
            TableVersion = new TableVersion("2024.01", "SP"),
            Classification = new NcmClassification("G1", "SG1", "S1", "S1.1"),
            Validity = new ValidityPeriod(new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31)),
            TaxableUnit = new TaxableUnit("KG", "Quilograma"),
            BenefitCode = new BenefitCode("123")
        };

        var result = await handler.Handle(command, CancellationToken.None);

        await repository.Received(1).SaveAsync(Arg.Any<Ncm>());
        await uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBeEmpty();
    }
}

using Administration.Application.Ncms.Commands.UpdateNcm;
using Administration.Domain.Ncms;
using Administration.Domain.Ncms.ValueObjects;
using BuildingBlock.Application;

namespace Administration.Application.UnitTest.Ncms.Commands;

public class UpdateNcmHandlerTests
{
    [Fact]
    public async Task Handle_WhenFound_ShouldUpdate()
    {
        var repository = Substitute.For<INcmRepository>();
        var uow = Substitute.For<IUnitOfWork>();
        var handler = new UpdateNcmHandler(repository, uow);
        var existing = Ncm.Create(new NcmDto(
            "Pizza congelada",
            new NcmCode("19059090"),
            new TaxationProfile("Alimentos", 1.25m),
            new TaxEstimation(1.5m, 2.5m, 3.5m, 4.5m),
            new TableVersion("2024.01", "SP"),
            new NcmClassification("G1", "SG1", "S1", "S1.1"),
            new ValidityPeriod(new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31)),
            new TaxableUnit("KG", "Quilograma"),
            new BenefitCode("123")));
        repository.GetByIdAsync(existing.Id).Returns(existing);

        var command = new UpdateNcmCommand
        {
            Id = existing.Id,
            Description = "Molho pronto",
            Code = new NcmCode("21039010"),
            TaxationProfile = new TaxationProfile("Condimentos", 0.75m),
            TaxEstimation = new TaxEstimation(0.5m, 1.5m, 2.5m, 3.5m),
            TableVersion = new TableVersion("2024.02", "RJ"),
            Classification = new NcmClassification("G2", "SG2", "S2", "S2.1"),
            Validity = new ValidityPeriod(new DateOnly(2024, 2, 1), null),
            TaxableUnit = new TaxableUnit("L", "Litro"),
            BenefitCode = new BenefitCode("456")
        };

        var result = await handler.Handle(command, CancellationToken.None);

        await repository.Received(1).SaveAsync(existing);
        await uow.Received(1).CommitAsync(Arg.Any<CancellationToken>());
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenNotFound_ShouldFail()
    {
        var repository = Substitute.For<INcmRepository>();
        var uow = Substitute.For<IUnitOfWork>();
        var handler = new UpdateNcmHandler(repository, uow);
        var command = new UpdateNcmCommand { Id = Guid.NewGuid(), Description = "Item", Code = new NcmCode("21039010"), TaxationProfile = new TaxationProfile("Condimentos", null), TaxEstimation = new TaxEstimation(0, 0, 0, 0), TableVersion = new TableVersion("2024.02", null), Classification = new NcmClassification(null, null, null, null), Validity = new ValidityPeriod(new DateOnly(2024, 1, 1), null) };

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        await repository.DidNotReceive().SaveAsync(Arg.Any<Ncm>());
        await uow.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}

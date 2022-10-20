using desafio.feiras.application.tests.Mocks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation.TestHelper;
using desafio.feiras.application.Command.RemoveFeira;

namespace desafio.feiras.application.tests.Commands
{
    public class RemoveFeiraTest
    {
        [Fact(DisplayName = "Remove feira - RemoveFeiraCommand")]
        public async Task RemoveExistentFeira()
        {
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<RemoveFeiraCommand>();

            var service = FeiraServiceMock.BuildRemoveFeiraServiceMock();

            var command = new RemoveFeiraCommand(service.Object, logger);

            var response = await command.Handle(new RemoveFeiraRequest { Identificador = 1 }, CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<MediatR.Unit>();
        }

        [Fact(DisplayName = "Valida se a feira nao existe")]
        public async Task ValidateRemoveFeiraIdNotExists()
        {
            var service = FeiraServiceMock.BuildRemoveFeiraServiceMock();

            var validator = new RemoveFeiraRequestValidator(service.Object);

            var feira = new RemoveFeiraRequest { Identificador = 2 };

            var result = await validator.TestValidateAsync(feira);

            result.ShouldHaveValidationErrorFor(x => x.Identificador).WithErrorMessage("O identificador de feira informado não existe.");
        }
    }
}
using desafio.feiras.application.Command.RemoveFeira;
using desafio.feiras.application.tests.Mocks;
using desafio.feiras.application.Validation;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using Shouldly;

namespace desafio.feiras.application.tests.Validation
{
    public class ValidationTest
    {
        [Fact(DisplayName = "Testa o comportamento do middleware de validação com excecao")]
        public async Task ExecuteBehaviorHandleException()
        {
            var service = FeiraServiceMock.BuildRemoveFeiraServiceMock();
            var validator = new RemoveFeiraRequestValidator(service.Object);

            var behavior = new ValidationBehavior<RemoveFeiraRequest, MediatR.Unit>(new IValidator<RemoveFeiraRequest>[] { validator.As<IValidator<RemoveFeiraRequest>>() });

            var exception = await Should.ThrowAsync<application.Validation.ValidationException>(async () =>
            {
                var request = new RemoveFeiraRequest { Identificador = 2 };
                var requestHandler = new Mock<MediatR.RequestHandlerDelegate<MediatR.Unit>>();
                requestHandler.Setup(x => x.Invoke()).Returns(() => Task.FromResult(MediatR.Unit.Value));

                await behavior.Handle(request, requestHandler.Object, CancellationToken.None);
            });

            exception.Should().NotBeNull();
            exception.Should().BeOfType<application.Validation.ValidationException>();
            exception?.Errors?.Count.Should().Be(1);
        }

        [Fact(DisplayName = "Testa o comportamento do middleware de validação sem excecao")]
        public async Task ExecuteBehaviorHandleSuccess()
        {
            var service = FeiraServiceMock.BuildRemoveFeiraServiceMock();
            var validator = new RemoveFeiraRequestValidator(service.Object);

            var behavior = new ValidationBehavior<RemoveFeiraRequest, MediatR.Unit>(new IValidator<RemoveFeiraRequest>[] { validator.As<IValidator<RemoveFeiraRequest>>() });

            var request = new RemoveFeiraRequest { Identificador = 1 };
            var requestHandler = new Mock<MediatR.RequestHandlerDelegate<MediatR.Unit>>();
            requestHandler.Setup(x => x.Invoke()).Returns(() => Task.FromResult(MediatR.Unit.Value));

            var result = await behavior.Handle(request, requestHandler.Object, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeOfType<MediatR.Unit>();
        }

        [Fact(DisplayName = "Testa a excecao ValidationException")]
        public Task ValidationExceptionSimple()
        {
            var exception = new application.Validation.ValidationException("Mensagem de validação", "Erro 1");

            exception?.Message.Should().BeEquivalentTo("Mensagem de validação");
            exception?.Errors?.Count.Should().Be(1);

            return Task.CompletedTask;
        }
    }
}

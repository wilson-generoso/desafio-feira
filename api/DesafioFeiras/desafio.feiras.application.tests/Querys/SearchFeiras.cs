using AutoMapper;
using desafio.feiras.application.tests.Mocks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation.TestHelper;
using desafio.feiras.application.Query.SearchFeiras;

namespace desafio.feiras.application.tests.Querys
{
    public class SearchFeirasTest
    {
        [Fact(DisplayName = "Pesquisa todas as feiras sem filtro - SearchFeirasQuery")]
        public async Task SearchAllFeiras()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SearchFeirasQuery>();

            var service = FeiraServiceMock.BuildSearchFeirasServiceMock();

            var command = new SearchFeirasQuery(service.Object, logger, mapper);

            var response = await command.Handle(new SearchFeirasRequest(), CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<SearchFeirasResponse>();
            response?.Feiras.Count().Should().Be(5);
        }

        [Fact(DisplayName = "Pesquisa feiras por distrito - SearchFeirasQuery")]
        public async Task SearchFeirasByDistrito()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SearchFeirasQuery>();

            var service = FeiraServiceMock.BuildSearchFeirasServiceMock();

            var command = new SearchFeirasQuery(service.Object, logger, mapper);

            var response = await command.Handle(new SearchFeirasRequest() { DistritoMunicipal = "bbb" }, CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<SearchFeirasResponse>();
            response?.Feiras.Count().Should().Be(2);
        }

        [Fact(DisplayName = "Pesquisa feiras por regiao 5 - SearchFeirasQuery")]
        public async Task SearchFeirasByRegiao5()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SearchFeirasQuery>();

            var service = FeiraServiceMock.BuildSearchFeirasServiceMock();

            var command = new SearchFeirasQuery(service.Object, logger, mapper);

            var response = await command.Handle(new SearchFeirasRequest() { RegiaoMunicipio5Areas = "xxx" }, CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<SearchFeirasResponse>();
            response?.Feiras?.Count().Should().Be(3);
        }

        [Fact(DisplayName = "Pesquisa feiras por nome - SearchFeirasQuery")]
        public async Task SearchFeirasByNome()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SearchFeirasQuery>();

            var service = FeiraServiceMock.BuildSearchFeirasServiceMock();

            var command = new SearchFeirasQuery(service.Object, logger, mapper);

            var response = await command.Handle(new SearchFeirasRequest() { Nome = "d" }, CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<SearchFeirasResponse>();
            response?.Feiras.Count().Should().Be(2);
        }

        [Fact(DisplayName = "Pesquisa feiras por bairro - SearchFeirasQuery")]
        public async Task SearchFeirasByBairro()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<SearchFeirasQuery>();

            var service = FeiraServiceMock.BuildSearchFeirasServiceMock();

            var command = new SearchFeirasQuery(service.Object, logger, mapper);

            var response = await command.Handle(new SearchFeirasRequest() { Bairro = "1" }, CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<SearchFeirasResponse>();
            response?.Feiras.Count().Should().Be(2);
        }

        [Fact(DisplayName = "Valida se filtro de pesquisa foi informado")]
        public async Task ValidateSearchFilterInformed()
        {
            var validator = new SearchFeirasRequestValidator();

            var feira = new SearchFeirasRequest();

            var result = await validator.TestValidateAsync(feira);

            result.ShouldHaveValidationErrorFor(x => x).WithErrorMessage("Informe ao menos um filtro para pesquisa.");
        }

        [Fact(DisplayName = "Valida tamanho do dado informado no filtro da pesquisa por feira")]
        public async Task ValidateSearchFeiraLength()
        {
            var validator = new SearchFeirasRequestValidator();

            var feira = new SearchFeirasRequest()
            {
                DistritoMunicipal = "".PadRight(19, 'a'),
                RegiaoMunicipio5Areas = "".PadRight(7, 'a'),
                Nome = "".PadRight(31, 'a'),
                Bairro = "".PadRight(21, 'a'),
            };

            var result = await validator.TestValidateAsync(feira);

            result.ShouldHaveValidationErrorFor(x => x.DistritoMunicipal).WithErrorMessage("O nome do distrito municipal deve possuir no máximo 18 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.RegiaoMunicipio5Areas).WithErrorMessage("A região em 5 áreas deve possuir no máximo 6 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Nome).WithErrorMessage("O nome da feira livre deve possuir no máximo 30 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Bairro).WithErrorMessage("O bairro deve possuir no máximo 20 caracteres");
        }
    }
}
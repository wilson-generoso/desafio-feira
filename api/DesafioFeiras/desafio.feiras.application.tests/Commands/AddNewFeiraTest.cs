using AutoMapper;
using desafio.feiras.application.Command.AddNewFeira;
using desafio.feiras.application.tests.Mocks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace desafio.feiras.application.tests.Commands
{
    public class AddNewFeiraTest
    {
        [Fact(DisplayName = "Adiciona uma nova feira - AddNewFeiraCommand")]
        public async Task AddNewCorrectFeira()
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile<ApplicationMapping>());
            var mapper = mapperConfig.CreateMapper();

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<AddNewFeiraCommand>();

            var service = FeiraServiceMock.BuildAddNewFeiraServiceMock();

            var command = new AddNewFeiraCommand(service.Object, mapper, logger);

            var response = await command.Handle(new AddNewFeiraRequest(), CancellationToken.None);

            response.Should().NotBeNull();
            response.Should().BeOfType<AddNewFeiraResponse>();
            response?.Identificador.Should().Be(1);
        }

        [Fact(DisplayName = "Valida campos nulos ou vazios na requisicao de cadastro de nova feira")]
        public async Task ValidateNewFeiraNotNull()
        {
            var validator = new AddNewFeiraRequestValidator();

            var feira = new AddNewFeiraRequest() { Longitude = "", Bairro = "", AreaPonderacao = "" };

            var result = await validator.TestValidateAsync(feira);

            result.ShouldHaveValidationErrorFor(x => x.Longitude).WithErrorMessage("Informe a longitude");
            result.ShouldHaveValidationErrorFor(x => x.Latitude).WithErrorMessage("Informe a latitude");
            result.ShouldHaveValidationErrorFor(x => x.SetorCensitario).WithErrorMessage("Informe o setor censit�rio");
            result.ShouldHaveValidationErrorFor(x => x.AreaPonderacao).WithErrorMessage("Informe a �rea de pondera��o");
            result.ShouldHaveValidationErrorFor(x => x.CodigoDistritoIBGE).WithErrorMessage("Informe o c�digo do distrito conforme IBGE");
            result.ShouldHaveValidationErrorFor(x => x.DistritoMunicipal).WithErrorMessage("Informe o nome do distrito municipal");
            result.ShouldHaveValidationErrorFor(x => x.CodigoSubprefeitura).WithErrorMessage("Informe o c�digo de uma das 31 subprefeituras (2003 a 2012)");
            result.ShouldHaveValidationErrorFor(x => x.Subprefeitura).WithErrorMessage("Informe o nome da Subprefeitura (31 de 2003 at� 2012)");
            result.ShouldHaveValidationErrorFor(x => x.RegiaoMunicipio5Areas).WithErrorMessage("Informe a regi�o conforme divis�o do Munic�pio em 5 �reas");
            result.ShouldHaveValidationErrorFor(x => x.RegiaoMunicipio8Areas).WithErrorMessage("Informe a regi�o conforme divis�o do Munic�pio em 8 �reas");
            result.ShouldHaveValidationErrorFor(x => x.Nome).WithErrorMessage("Informe o nome da feira livre atribu�da pela Supervis�o de Abastecimento");
            result.ShouldHaveValidationErrorFor(x => x.Registro).WithErrorMessage("Informe o n�mero do registro da feira livre na PMSP");
            result.ShouldHaveValidationErrorFor(x => x.Logradouro).WithErrorMessage("Informe o nome do logradouro onde se localiza a feira livre");
        }

        [Fact(DisplayName = "Valida tamanho do dado na requisicao de cadastro de nova feira")]
        public async Task ValidateNewFeiraLength()
        {
            var validator = new AddNewFeiraRequestValidator();

            var feira = new AddNewFeiraRequest()
            {
                Longitude = "".PadRight(11, 'a'),
                Latitude = "".PadRight(11, 'a'),
                SetorCensitario = "".PadRight(16, 'a'),
                AreaPonderacao = "".PadRight(14, 'a'),
                CodigoDistritoIBGE = "".PadRight(10, 'a'),
                DistritoMunicipal = "".PadRight(19, 'a'),
                CodigoSubprefeitura = "".PadRight(3, 'a'),
                Subprefeitura = "".PadRight(26, 'a'),
                RegiaoMunicipio5Areas = "".PadRight(7, 'a'),
                RegiaoMunicipio8Areas = "".PadRight(8, 'a'),
                Nome = "".PadRight(31, 'a'),
                Registro = "".PadRight(7, 'a'),
                Logradouro = "".PadRight(35, 'a'),
                NumeroLogradouro = "".PadRight(6, 'a'),
                Bairro = "".PadRight(21, 'a'),
                PontoReferencia = "".PadRight(25, 'a'),
            };

            var result = await validator.TestValidateAsync(feira);

            result.ShouldHaveValidationErrorFor(x => x.Longitude).WithErrorMessage("A longitude deve possuir no m�ximo 10 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Latitude).WithErrorMessage("A latitude deve possuir no m�ximo 10 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.SetorCensitario).WithErrorMessage("O setor censit�rio deve possuir no m�ximo 15 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.AreaPonderacao).WithErrorMessage("A �rea de pondera��o deve possuir no m�ximo 13 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.CodigoDistritoIBGE).WithErrorMessage("O c�digo do distrito conforme IBGE deve possuir no m�ximo 9 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.DistritoMunicipal).WithErrorMessage("O nome do distrito municipal deve possuir no m�ximo 18 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.CodigoSubprefeitura).WithErrorMessage("O c�digo de subprefeituras deve possuir no m�ximo 2 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Subprefeitura).WithErrorMessage("O nome da Subprefeitura deve possuir no m�ximo 25 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.RegiaoMunicipio5Areas).WithErrorMessage("A regi�o em 5 �reas deve possuir no m�ximo 6 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.RegiaoMunicipio8Areas).WithErrorMessage("A regi�o em 8 �reas deve possuir no m�ximo 7 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Nome).WithErrorMessage("O nome da feira livre deve possuir no m�ximo 30 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Registro).WithErrorMessage("O n�mero do registro deve possuir no m�ximo 6 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Logradouro).WithErrorMessage("O nome do logradouro deve possuir no m�ximo 34 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.NumeroLogradouro).WithErrorMessage("O n�mero do logradouro deve possuir no m�ximo 5 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.Bairro).WithErrorMessage("O bairro deve possuir no m�ximo 20 caracteres");
            result.ShouldHaveValidationErrorFor(x => x.PontoReferencia).WithErrorMessage("O ponto de refer�ncia deve possuir no m�ximo 24 caracteres");
        }
    }
}
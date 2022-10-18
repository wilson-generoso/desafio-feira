using desafio.feiras.infrastructure;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.application.Command.UpdateFeira
{
    /// <summary>
    /// Dados de solicitação para atualização de feira
    /// </summary>
    public class UpdateFeiraRequest : domain.Feira, IRequest
    {
    }

    /// <summary>
    /// Validador de dados de solicitação para atualização de feira
    /// </summary>
    public class UpdateFeiraRequestValidator : AbstractValidator<UpdateFeiraRequest>
    {
        private readonly IFeiraService service;

        /// <summary>
        /// Configura regras de validação
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para validação de identificador de feira</param>
        public UpdateFeiraRequestValidator(IFeiraService service)
        {
            this.service = service;

            RuleFor(x => x.Identificador)
                .MustAsync(HasFeira)
                .WithMessage("O identificador de feira informado não existe.");

            RuleFor(a => a.Longitude)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a longitude")
                .MaximumLength(10).WithMessage("A longitude deve possuir no máximo 10 caracteres");

            RuleFor(a => a.Latitude)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a latitude")
                .MaximumLength(10).WithMessage("A latitude deve possuir no máximo 10 caracteres");

            RuleFor(a => a.SetorCensitario)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o setor censitário")
                .MaximumLength(15).WithMessage("O setor censitário deve possuir no máximo 15 caracteres");

            RuleFor(a => a.AreaPonderacao)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a área de ponderação")
                .MaximumLength(13).WithMessage("A área de ponderação deve possuir no máximo 13 caracteres");

            RuleFor(a => a.CodigoDistritoIBGE)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o código do distrito conforme IBGE")
                .MaximumLength(9).WithMessage("O código do distrito conforme IBGE deve possuir no máximo 9 caracteres");

            RuleFor(a => a.DistritoMunicipal)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome do distrito municipal")
                .MaximumLength(18).WithMessage("O nome do distrito municipal deve possuir no máximo 18 caracteres");

            RuleFor(a => a.CodigoSubprefeitura)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o código de uma das 31 subprefeituras (2003 a 2012)")
                .MaximumLength(2).WithMessage("O código de subprefeituras deve possuir no máximo 2 caracteres");

            RuleFor(a => a.Subprefeitura)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome da Subprefeitura (31 de 2003 até 2012)")
                .MaximumLength(25).WithMessage("O nome da Subprefeitura deve possuir no máximo 25 caracteres");

            RuleFor(a => a.RegiaoMunicipio5Areas)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a região conforme divisão do Município em 5 áreas")
                .MaximumLength(6).WithMessage("A região em 5 áreas deve possuir no máximo 6 caracteres");

            RuleFor(a => a.RegiaoMunicipio8Areas)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a região conforme divisão do Município em 8 áreas")
                .MaximumLength(7).WithMessage("A região em 8 áreas deve possuir no máximo 7 caracteres");

            RuleFor(a => a.Nome)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome da feira livre atribuída pela Supervisão de Abastecimento")
                .MaximumLength(30).WithMessage("O nome da feira livre deve possuir no máximo 30 caracteres");

            RuleFor(a => a.Registro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o número do registro da feira livre na PMSP")
                .MaximumLength(6).WithMessage("O número do registro deve possuir no máximo 6 caracteres");

            RuleFor(a => a.Logradouro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome do logradouro onde se localiza a feira livre")
                .MaximumLength(34).WithMessage("O nome do logradouro deve possuir no máximo 34 caracteres");

            RuleFor(a => a.NumeroLogradouro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe um número do logradouro onde se localiza a feira livre")
                .MaximumLength(5).WithMessage("O número do logradouro deve possuir no máximo 5 caracteres");

            RuleFor(a => a.Bairro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o bairro de localização da feira livre")
                .MaximumLength(20).WithMessage("O bairro deve possuir no máximo 20 caracteres");

            RuleFor(a => a.PontoReferencia)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o ponto de referência da localização da feira livre")
                .MaximumLength(24).WithMessage("O ponto de referência deve possuir no máximo 24 caracteres");
        }

        /// <summary>
        /// Verifica se a feira existe
        /// </summary>
        /// <param name="identificador">Identificador da feira</param>
        /// <param name="token"></param>
        /// <returns>Indicador de existência ou não da feira</returns>
        private async Task<bool> HasFeira(int identificador, CancellationToken token)
        {
            return await service.Exists(identificador);
        }

    }
}

using desafio.feiras.domain;

namespace desafio.feiras.infrastructure
{
    public interface IFeiraService : IService<Feira>
    {
        Task<bool> Exists(int id);

        Task<IEnumerable<Feira>> SearchForFeiras(string distritoMunicipal, string regiaoMunicipio5Areas, string nome, string bairro);
    }
}

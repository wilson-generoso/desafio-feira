using desafio.feiras.domain;
using desafio.feiras.infrastructure;
using Moq;

namespace desafio.feiras.application.tests.Mocks
{
    public static class FeiraServiceMock
    {
        public static Mock<IFeiraService> BuildAddNewFeiraServiceMock()
        {
            var mock = new Mock<IFeiraService>();

            mock.Setup(x => x.Create(It.IsAny<Feira>())).Returns((Feira feira) => { return Task.FromResult(1); });

            return mock;
        }
        public static Mock<IFeiraService> BuildRemoveFeiraServiceMock()
        {
            var mock = new Mock<IFeiraService>();

            mock.Setup(x => x.Delete(It.IsAny<int>())).Returns((int id) => { return Task.CompletedTask; });
            mock.Setup(x => x.Exists(It.IsAny<int>())).Returns((int id) => { return Task.FromResult(id == 1); });

            return mock;
        }

        public static Mock<IFeiraService> BuildUpdateFeiraServiceMock()
        {
            var mock = new Mock<IFeiraService>();

            mock.Setup(x => x.Update(It.IsAny<Feira>())).Returns((Feira feira) => { return Task.CompletedTask; });
            mock.Setup(x => x.Exists(It.IsAny<int>())).Returns((int id) => { return Task.FromResult(id == 1); });

            return mock;
        }

        public static Mock<IFeiraService> BuildSearchFeirasServiceMock()
        {
            var mock = new Mock<IFeiraService>();

            var feiras = new Feira[]
            {
                new Feira { Identificador = 1, DistritoMunicipal = "aaa", RegiaoMunicipio5Areas = "xxx", Nome = "abcd", Bairro = "000" },
                new Feira { Identificador = 2, DistritoMunicipal = "bbb", RegiaoMunicipio5Areas = "xxx", Nome = "adef", Bairro = "000" },
                new Feira { Identificador = 3, DistritoMunicipal = "bbb", RegiaoMunicipio5Areas = "xxx", Nome = "cghi", Bairro = "111" },
                new Feira { Identificador = 4, DistritoMunicipal = "ccc", RegiaoMunicipio5Areas = "yyy", Nome = "cjkl", Bairro = "111" },
                new Feira { Identificador = 5, DistritoMunicipal = "ccc", RegiaoMunicipio5Areas = "yyy", Nome = "zmno", Bairro = "222" },
            };

            mock.Setup(x => x.SearchForFeiras(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string distritoMunicipal, string regiaoMunicipio5Areas, string nome, string bairro) =>
                {
                    var feirasFound = feiras.AsEnumerable();

                    if (!string.IsNullOrEmpty(distritoMunicipal))
                        feirasFound = feirasFound.Where(x => x.DistritoMunicipal == distritoMunicipal);

                    if (!string.IsNullOrEmpty(regiaoMunicipio5Areas))
                        feirasFound = feirasFound.Where(x => x.RegiaoMunicipio5Areas == regiaoMunicipio5Areas);

                    if (!string.IsNullOrEmpty(nome))
                        feirasFound = feirasFound.Where(x => x.Nome.Contains(nome));

                    if (!string.IsNullOrEmpty(bairro))
                        feirasFound = feirasFound.Where(x => x.Bairro.Contains(bairro));

                    return Task.FromResult(feirasFound);
                });

            return mock;
        }
    }
}

# desafio-feiras

## Proposta
Desenvolver uma API que exponha os dados disponíveis em [1] utilizando uma
abordagem orientada arecursos e que atenda os requisitos listados abaixo.

### Escopo
Utilizando os dados do arquivo [DEINFO_AB_FEIRASLIVRES_2014.csv](http://www.prefeitura.sp.gov.br/cidade/secretarias/upload/chamadas/feiras_livres_1429113213.zip), implemente:
- cadastro de uma nova feira;
- exclusão de uma feira através de seu código de registro;
- alteração dos campos cadastrados de uma feira, exceto seu código de registro;
- busca de feiras utilizando ao menos um dos parâmetros abaixo:
-- distrito
-- regiao5
-- nome_feira
-- bairro

### Requisitos
Utilize git ou hg para fazer o controle de versão da solução do teste e hospede-a
no Github ou Bitbucket; não utilize o nome da empresa, em pacotes, arquivos ou no nome do
repositório.
- Armazene os dados fornecidos pela Prefeitura de São Paulo em um banco de dados que você julgarapropriado; 
- A solução deve conter um script para importar os dados do arquivo DEINFO_AB_FEIRASLIVRES_2014.csv para o banco de dados; 
- A API deve seguir os conceitos REST;
- O Content-Type das respostas da API deve ser application/json;
- O código da solução deve conter testes e algum mecanismo documentado para gerar a informação de cobertura dos testes;
- A aplicação deve gravar logs estruturados em arquivos texto;
- A solução desta avaliação deve estar documentada em português ou inglês. Escolha um idioma em que você seja fluente;
- A documentação da solução do teste deve incluir como rodar o projeto;
- e exemplos de requisições e suas possíveis respostas;

## Solução

- A solução foi construída em modelo de microserviços com tecnologia Microsoft.NET 6 conectado a um banco de dados NoSQL MongoDB, todos em conteineres. 
- Foi adotado Clean Architecture como padrão arquitetural da solução, nas seguintes camadas:
	- API: Responsável por expor as API's em modelo REST (AspNetCore WebApi MVC)
	- Application: camada de aplicação modelada utilizando-se do Pattern Mediator CQRS (Command Query Responsability Segregation), que por sua vez contribui fortemente na utilização dos padrões SOLID
	- Domain: camada com as entidades básicas da solução
	- Infrastructure: camada de abstração para serviços de infraestrutura
	- Infrastructure MongoDB: camada de infraestrutura com os repositórios de acesso ao MongoDB
- Para leitura e carga do arquivo DEINFO_AB_FEIRASLIVRES_2014.csv foi utilizada linguagem Python.
- Foram criados testes unitários utilizando-se do framework xUnit com FluentAssertions e Moq para as camadas: Application e Infraestruture. Para cobertura de testes foi utilizado o componente [covertlet.msbuild](https://github.com/coverlet-coverage/coverlet), que gera um arquivo "coverage.json" com a documentação detalhada de cobertura dos testes unitários.
- Para escrita de logs em arquivo, foram implementadas classes utilizando-se da abstração da própria Microsoft (Microsoft.Extensions.Logging.Abstraction).
- Utilizamos Swagger para documentação dos endpoints da API

### Instruções para subir a aplicação local

#### Pré-requisitos

Para o funcionamento da aplicação é necessário a instalação dos seguintes pré-requisitos:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/) (durante a instalação ative o componente WSL 2 para suporte a container linux)
2. [Dotnet 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
3. [Python 3.7.8+](https://www.python.org/downloads/)
4. [PIP para Python](https://pip.pypa.io/en/stable/installation/#get-pip-py)

#### Subindo o ambiente

1. Clone o repositório do "Desafio Feiras" executando a instrução:
	```
	git clone https://github.com/wilson-generoso/desafio-feira.git
	```

2. Entre na pasta api/DesafioFeiras (no contexto da pasta clonada):
	```
	cd api/DesafioFeiras
	```

3. Compile a aplicação:
	```
	dotnet build DesafioFeiras.sln
	```

4. Execute o teste da aplicação para verificar a cobertura de codigo:
	```
	dotnet test DesafioFeiras.sln /p:CollectCoverage=true
	```

5. Execute a instrução abaixo para inicializar a aplicação (este comando deixa a console ocupada enquanto a aplicação estiver ativa, para finalizar a aplicação pressione CTRL+C):
	```
	docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" --ansi never up --build --remove-orphans
	```

4. Abra a url da [documentação da API](https://localhost:8001/swagger) em https://localhost:8001/swagger.

#### Rodando script de carga do arquivo de feiras

**IMPORTANTE!**
- Antes de iniciar a execução da carga deve-se garantir que o módulo *requests* tenha sido instalado, caso não tenha sido instalado execute a instrução:
	```
	pip install requests
	```
- Certifique que a aplicação esteja em execução

Execute os passos a seguir:

1. Entre na pasta scripts (no contexto da pasta clonada):
	```
	cd scripts
	```

2. Execute o script de carregamento do arquivo (_certifique que a aplicação esteja em execução_):
	```
	py loadFeiras.py
	```
	OU
	```
	python loadFeiras.py
	```

#### Utilizando a API

- Para registrar uma nova feira, você deve usar o endpoint [POST]/api/v1/Feira. Veja um exemplo:
	```
	curl --location --request POST 'https://localhost:8001/api/v1/Feira' \
		 --header 'Content-Type: application/json' \
		 --data-raw '{"longitude": "-46550164", "latitude": "-23558733", "setorCensitario": "355030885000091", "areaPonderacao": "3550308005040", "codigoDistritoIBGE": "87", "distritoMunicipal": "VILA FORMOSA", "codigoSubprefeitura": "26", "subprefeitura": "ARICANDUVA-FORMOSA-CARRAO", "regiaoMunicipio5Areas": "Leste", "regiaoMunicipio8Areas": "Leste 1", "nome": "VILA FORMOSA", "registro": "4041-0", "logradouro": "RUA MARAGOJIPE", "numeroLogradouro": "S/N", "bairro": "VL FORMOSA", "pontoReferencia": "TV RUA PRETORIA"}'
	```
- Para atualizar uma feira já registrada, voce deve usar o endpoint [PUT]/api/v1/Feira, adicionando o _identificador_ desejado. Veja um exemplo: 
	```
	curl --location --request PUT 'https://localhost:8001/api/v1/Feira' \
		 --header 'Content-Type: application/json' \
		 --data-raw '{"identificador":"1604", "longitude": "-46550164", "latitude": "-23558733", "setorCensitario": "355030885000091", "areaPonderacao": "3550308005040", "codigoDistritoIBGE": "87", "distritoMunicipal": "VILA FORMOSA", "codigoSubprefeitura": "26", "subprefeitura": "ARICANDUVA-FORMOSA-CARRO", "regiaoMunicipio5Areas": "Leste", "regiaoMunicipio8Areas": "Leste 1", "nome": "VILA FORMOSA", "registro": "4041-0", "logradouro": "RUA MARAGOJIPE", "numeroLogradouro": "S/N", "bairro": "VL FORMOSA", "pontoReferencia": "TV RUA PRETORIA"}'
	```

- Para remover uma feira já registrada, voce deve usar o endpoint [DELETE]/api/v1/Feira/{identificador}, adicionando o _identificador_ desejado na url. Veja um exemplo: 
	```
	curl --location --request DELETE 'https://localhost:8001/api/v1/Feira/1604'
	```

- Para pesquisar por feiras registradas, voce deve usar o endpoint [GET]/api/v1/Feira, adicionando ao menos um dos filtros a seguir:
	- distrito
	- regiao5
	- nome_feira
	- bairro

. Veja um exemplo: 
	```
	curl --location --request GET 'https://localhost:8001/api/v1/Feira?distrito=SANTANA&regiao5=Norte&nome_feira=LAUZANE&bairro=STA'
	```

- Para verificar os arquivos de logs, execute a instrução a seguir:
	```
	cd %APPDATA%/DesafioFeiras/Log
	```

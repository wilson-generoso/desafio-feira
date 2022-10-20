# desafio-feiras

## Proposta
Desenvolver uma API que exponha os dados dispon�veis em [1] utilizando uma
abordagem orientada arecursos e que atenda os requisitos listados abaixo.

### Escopo
Utilizando os dados do arquivo [DEINFO_AB_FEIRASLIVRES_2014.csv](http://www.prefeitura.sp.gov.br/cidade/secretarias/upload/chamadas/feiras_livres_1429113213.zip), implemente:
- cadastro de uma nova feira;
- exclus�o de uma feira atrav�s de seu c�digo de registro;
- altera��o dos campos cadastrados de uma feira, exceto seu c�digo de registro;
- busca de feiras utilizando ao menos um dos par�metros abaixo:
-- distrito
-- regiao5
-- nome_feira
-- bairro

### Requisitos
Utilize git ou hg para fazer o controle de vers�o da solu��o do teste e hospede-a
no Github ou Bitbucket; n�o utilize o nome da empresa, em pacotes, arquivos ou no nome do
reposit�rio.
- Armazene os dados fornecidos pela Prefeitura de S�o Paulo em um banco de dados que voc� julgarapropriado; 
- A solu��o deve conter um script para importar os dados do arquivo DEINFO_AB_FEIRASLIVRES_2014.csv para o banco de dados; 
- A API deve seguir os conceitos REST;
- O Content-Type das respostas da API deve ser application/json;
- O c�digo da solu��o deve conter testes e algum mecanismo documentado para gerar a informa��o de cobertura dos testes;
- A aplica��o deve gravar logs estruturados em arquivos texto;
- A solu��o desta avalia��o deve estar documentada em portugu�s ou ingl�s. Escolha um idioma em que voc� seja fluente;
- A documenta��o da solu��o do teste deve incluir como rodar o projeto;
- e exemplos de requisi��es e suas poss�veis respostas;

## Solu��o

- A solu��o foi constru�da em modelo de microservi�os com tecnologia Microsoft.NET 6 conectado a um banco de dados NoSQL MongoDB, todos em conteineres. 
- Foi adotado Clean Architecture como padr�o arquitetural da solu��o, nas seguintes camadas:
	- API: Respons�vel por expor as API's em modelo REST (AspNetCore WebApi MVC)
	- Application: camada de aplica��o modelada utilizando-se do Pattern Mediator CQRS (Command Query Responsability Segregation), que por sua vez contribui fortemente na utiliza��o dos padr�es SOLID
	- Domain: camada com as entidades b�sicas da solu��o
	- Infrastructure: camada de abstra��o para servi�os de infraestrutura
	- Infrastructure MongoDB: camada de infraestrutura com os reposit�rios de acesso ao MongoDB
- Para leitura e carga do arquivo DEINFO_AB_FEIRASLIVRES_2014.csv foi utilizada linguagem Python.
- Foram criados testes unit�rios utilizando-se do framework xUnit com FluentAssertions e Moq para as camadas: Application e Infraestruture. Para cobertura de testes foi utilizado o componente [covertlet.msbuild](https://github.com/coverlet-coverage/coverlet), que gera um arquivo "coverage.json" com a documenta��o detalhada de cobertura dos testes unit�rios.
- Para escrita de logs em arquivo, foram implementadas classes utilizando-se da abstra��o da pr�pria Microsoft (Microsoft.Extensions.Logging.Abstraction).
- Utilizamos Swagger para documenta��o dos endpoints da API

### Instru��es para subir a aplica��o local

#### Pr�-requisitos

Para o funcionamento da aplica��o � necess�rio a instala��o dos seguintes pr�-requisitos:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/) (durante a instala��o ative o componente WSL 2 para suporte a container linux)
2. [Dotnet 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
3. [Python 3.7.8+](https://www.python.org/downloads/)
4. [PIP para Python](https://pip.pypa.io/en/stable/installation/#get-pip-py)

#### Subindo o ambiente

1. Clone o reposit�rio do "Desafio Feiras" executando a instru��o:
	```
	git clone https://github.com/wilson-generoso/desafio-feira.git
	```

2. Entre na pasta api/DesafioFeiras (no contexto da pasta clonada):
	```
	cd api/DesafioFeiras
	```

3. Compile a aplica��o:
	```
	dotnet build DesafioFeiras.sln
	```

4. Execute o teste da aplica��o para verificar a cobertura de codigo:
	```
	dotnet test DesafioFeiras.sln /p:CollectCoverage=true
	```

5. Execute a instru��o abaixo para inicializar a aplica��o (este comando deixa a console ocupada enquanto a aplica��o estiver ativa, para finalizar a aplica��o pressione CTRL+C):
	```
	docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" --ansi never up --build --remove-orphans
	```

4. Abra a url da [documenta��o da API](https://localhost:8001/swagger) em https://localhost:8001/swagger.

#### Rodando script de carga do arquivo de feiras

**IMPORTANTE!**
- Antes de iniciar a execu��o da carga deve-se garantir que o m�dulo *requests* tenha sido instalado, caso n�o tenha sido instalado execute a instru��o:
	```
	pip install requests
	```
- Certifique que a aplica��o esteja em execu��o

Execute os passos a seguir:

1. Entre na pasta scripts (no contexto da pasta clonada):
	```
	cd scripts
	```

2. Execute o script de carregamento do arquivo (_certifique que a aplica��o esteja em execu��o_):
	```
	py loadFeiras.py
	```
	OU
	```
	python loadFeiras.py
	```

#### Utilizando a API

- Para registrar uma nova feira, voc� deve usar o endpoint [POST]/api/v1/Feira. Veja um exemplo:
	```
	curl --location --request POST 'https://localhost:8001/api/v1/Feira' \
		 --header 'Content-Type: application/json' \
		 --data-raw '{"longitude": "-46550164", "latitude": "-23558733", "setorCensitario": "355030885000091", "areaPonderacao": "3550308005040", "codigoDistritoIBGE": "87", "distritoMunicipal": "VILA FORMOSA", "codigoSubprefeitura": "26", "subprefeitura": "ARICANDUVA-FORMOSA-CARRAO", "regiaoMunicipio5Areas": "Leste", "regiaoMunicipio8Areas": "Leste 1", "nome": "VILA FORMOSA", "registro": "4041-0", "logradouro": "RUA MARAGOJIPE", "numeroLogradouro": "S/N", "bairro": "VL FORMOSA", "pontoReferencia": "TV RUA PRETORIA"}'
	```
- Para atualizar uma feira j� registrada, voce deve usar o endpoint [PUT]/api/v1/Feira, adicionando o _identificador_ desejado. Veja um exemplo: 
	```
	curl --location --request PUT 'https://localhost:8001/api/v1/Feira' \
		 --header 'Content-Type: application/json' \
		 --data-raw '{"identificador":"1604", "longitude": "-46550164", "latitude": "-23558733", "setorCensitario": "355030885000091", "areaPonderacao": "3550308005040", "codigoDistritoIBGE": "87", "distritoMunicipal": "VILA FORMOSA", "codigoSubprefeitura": "26", "subprefeitura": "ARICANDUVA-FORMOSA-CARRO", "regiaoMunicipio5Areas": "Leste", "regiaoMunicipio8Areas": "Leste 1", "nome": "VILA FORMOSA", "registro": "4041-0", "logradouro": "RUA MARAGOJIPE", "numeroLogradouro": "S/N", "bairro": "VL FORMOSA", "pontoReferencia": "TV RUA PRETORIA"}'
	```

- Para remover uma feira j� registrada, voce deve usar o endpoint [DELETE]/api/v1/Feira/{identificador}, adicionando o _identificador_ desejado na url. Veja um exemplo: 
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

- Para verificar os arquivos de logs, execute a instru��o a seguir:
	```
	cd %APPDATA%/DesafioFeiras/Log
	```

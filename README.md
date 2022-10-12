# desafio-inmetrics

Neste reposit�rio se encontra o c�digo de solu��o do desafio proposto pela InMetrics, conforme os seguites requisitos:

## Solu��o
Um comerciante precisa controlar o seu fluxo de caixa di�rio com os lan�amentos (d�bitos e cr�ditos), tamb�m precisa de um relat�rio que disponibilize o saldo di�rio consolidado.

## Requisitos de neg�cio
- Servi�o que fa�a o controle dos lan�amentos
- Servi�o do consolidado di�rio

## Requisitos t�cnicos
- Desenho da solu��o
- Pode ser feito na linguagem que voc� domina
- Boas pr�ticas s�o bem vindas (Design Patterns, Padr�es de arquitetura, SOLID, etc)
- Readme com instru��es de como subir a aplica��o local, container e utiliza��o dos servi�os
- Hospedar em reposit�rio p�blico (GitHub)

#### Modelo de neg�cio
A solu��o desenvolvida se resume a duas APIs REST para registro de conta e lan�amentos.
Foi idealizado a necessidade da exist�ncia de uma conta na qual s�o registrados os lan�amentos e todo o controle de saldo di�rio.

- A conta possui um nome e seu saldo
- Os lan�amentos s�o registrados com uma descri��o, valor, data e tipo (d�bito ou cr�dito)

#### Solu��o t�cnica
A tecnologia utilizada na solu��o foi AspNetCore WebApi (MVC pattern) em DotNet 6, em modelo de microservi�o em container, com alguns objetos largamente utilizados no mercado a saber:
- Swagger
- Mediator
- FluentValidator
- ApiVersion
- AutoMapper

Como solu��o de persist�ncia foi utilizado banco de dados MongoDB.

Foi adotado Clean Architecture como padr�o arquitetural, nas camadas:
- **WebApi**: camada respons�vel por expor as APIs REST)
- **Application**: camada de aplica��o modelada utilizando-se do Pattern Mediator CQRS (Command Query Responsability Segregation), que por sua vez contribui fortemente na utiliza��o dos padr�es SOLID
- **Domain**: camada com as entidades b�sicas da solu��o
- **Infrastructure**: camada de abstra��o para servi�os de infraestrutura
- **Infrastructure MongoDB**: camada de infraestrutura com os reposit�rios de acesso ao MongoDB

Foram utilizados alguns design patterns, a saber:
- Abstract Factory
- Dependency Injection
- Decorator
- Singleton
- Adapter
- Composite
- Mediator
- Command
- Async/Await

### Instru��es para subir a aplica��o local

#### Pr�-requisitos

Para o funcionamento da aplica��o � necess�rio a instala��o dos seguintes pr�-requisitos:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/)
2. [Dotnet 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)


#### Subindo o ambiente

1. Clone o reposit�rio do "Desafio Lan�amentos" executando a instru��o:
	git clone https://github.com/wilson-generoso/desafio-inmetrics.git

2. Entre na pasta api/DesafioLancamentos (no contexto da pasta clonada):
	cd api/DesafioLancementos

3. Execute a seguinte instru��o para inicializar a aplica��o:
	docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" --ansi never up --build --remove-orphans

4. Abra a url da [documenta��o da API](https://localhost:8091/swagger)

5. Consuma a API para atender ao requisitos do desafio (voc� pode utilizar a tela de documenta��o se desejar):
***Os comandos abaixo s�o exemplos executados em console***

- Crie uma conta atrav�s do endpoint [POST]/api/v1/Account
	curl --location --request POST 'https://localhost:8091/api/v1/Account' \
	--header 'Content-Type: application/json' \
	--data-raw '{
	  "name": "Teste",
	  "balance": 20.91
	}'

- Copie o "id" da conta criada, retornado na resposta, e registre um lan�amento de cr�dito
	curl --location --request POST 'https://localhost:8091/api/v1/Entry' \
	--header 'Content-Type: application/json' \
	--data-raw '{
	  "accountId": "{substitua aqui com o 'id' da conta}",
	  "description": "Primeiro lan�amento",
	  "date": "2022-10-10",
	  "value": 10,
	  "type": 2
	}'	
	
- Consulte o saldo da conta
	curl --location --request GET 'https://localhost:8091/api/v1/Account/{adicione aqui o 'id' da conta}'

- Registre novo lan�amento de d�bito
	curl --location --request POST 'https://localhost:8091/api/v1/Entry' \
	--header 'Content-Type: application/json' \
	--data-raw '{
	  "accountId": "76702b95-7c07-470e-a1fb-33e711a557b6",
	  "description": "Registro debito",
	  "date": "2022-10-10",
	  "value": 15.37,
	  "type": 1
	}'

- Consulte o extrato consolidado das opera��es da conta
	curl --location --request GET 'https://localhost:8091/api/v1/Entry/{adicione aqui o 'id' da conta}'
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
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
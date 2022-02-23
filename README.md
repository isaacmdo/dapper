# Acesso a dados com dapper

Nesse repositório é usado o conceito de acesso acesso a dados com Dapper usando SQLServer

# Criando o banco

Nesse projeto foi usado o Azure Sql Edge em um container no Docker, seguindo a própria instrução do pacote <a href="https://hub.docker.com/_/microsoft-azure-sql-edge">(SQL EDGE)</a>

Porém podemos rodar um servidor Sql Edge ou Sql Server, e executar os scripts que disponibilizei neste repositório (SCRIPTS)

# Iniciando o projeto

Dentro do arquivo Program.cs, temos a variavel de configuração do banco, indique as informações corretas para conexão ao banco que criamos com os scripts

# Utilizando o projeto

No mesmo arquivo Program.cs, no método main, temos a chamada para cada funcionalidade de acesso a dados com Dapper, retire o comentário de qual queria visualizar o comportamneto, e execute o projeto.

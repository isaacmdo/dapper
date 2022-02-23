# Acesso a dados com dapper

Nesse repositório é usado o conceito de acesso acesso a dados com Dapper usando SQLServer

# Criando o banco

Nesse projeto foi usado o Azure Sql Edge em um container no Docker, seguindo a própria instrução do pacote <a href="https://hub.docker.com/_/microsoft-azure-sql-edge">(SQL EDGE)</a>

Porém podemos rodar um servidor Sql Edge ou Sql Server, e executar os scripts que disponibilizei neste repositório  <a href="https://github.com/zWeeeeelll/dapper/tree/main/scripts-para-o-banco">(SCRIPTS)</a>

# Iniciando o projeto

Dentro do arquivo Program.cs, temos a constante ```connectionString``` de configuração do banco no método ```Main``` , indique as informações corretas para conexão ao banco que criamos com os scripts
```csharp
 const string connectionString = "Server=localhost,1433;Database=banco;User ID=sa;Password=YOURPASSWORD";
```

# Utilizando o projeto

No mesmo arquivo Program.cs, no método main, temos a chamada para cada funcionalidade de acesso a dados com Dapper, retire o comentário de qual queria visualizar o comportamneto, e execute o projeto.

```csharp
 static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=banco;User ID=sa;Password=YOURPASSWORD";

            using (var connection = new SqlConnection(connectionString))
            {
                //ListCategories(connection);
                //CreateCategory(connection);
                //UpdateCategory(connection);
                //DeleteCategory(connection)k
                //CreateManyCategory(connection);
                //ListCategories(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ExecuteScalar(connection);
                //ReadView(connection);
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //SelectIn(connection);
                //Like(connection, "api");
                Transaction(connection);

            }

            ;
        }
```

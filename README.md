# DatabaseAccess

O projeto DatabaseAccess oferece uma solução robusta e flexível para abstrair o acesso a diferentes tipos de bancos de dados em aplicações .NET, utilizando C#. Facilita a integração e a troca entre diferentes tecnologias de banco de dados. Na implementação atual temos conectividade com SQL Server, SQLite, ODBC, e OleDb, sem a necessidade de alterar o código da aplicação.

## Sobre o Repositório

Neste repositório são apresentados dois projetos:
- `DatabaseAccess` um projeto de Library .NET para abstração de conexão a multiplos tipos de banco de dados.
-`UnitTestSQLite` projeto com testes de unidade que realiza alguns testes na biblioteca DatabaseAccess utilizando um banco local SQLite.

### Pré-requistos e execução

Tendo o Visual Studio (2019) instalado abra o arquivo `DatabaseAccess.sln` e execute os testes de unidade.

## Características Principais

- **Abstração de Conexão**: Permite conexões com múltiplos tipos de bancos de dados através de uma interface comum.
- **Flexibilidade**: Facilita a mudança entre diferentes bancos de dados com mínimas alterações no código.
- **Facilidade de Uso**: Simplifica o código necessário para realizar operações de banco de dados, como consultas e atualizações.

## Classes Principais

- **`DbAccess`**: Classe responsável por executar operações no banco de dados. Utiliza uma conexão (`DbConnection`) que é configurada dinamicamente com base no tipo do banco de dados. [Ver código](./project/DatabaseAccess/DbAccess.cs)
- **`BuildDb`**: Classe estática que fornece métodos para criar objetos `DbConnection` específicos do tipo de banco de dados desejado, definidos pela enumeração `RecognizedTypes`. [Ver código](./project/DatabaseAccess/BuildDb.cs)

## Como Usar

### Configurando a Conexão

Para iniciar, é necessário criar uma instância da classe `DbAccess` especificando a string de conexão e o tipo de banco de dados:

```csharp
var dbAccess = new DbAccess("sua_string_de_conexao_aqui", BuildDb.RecognizedTypes.SqlClient);
```

### Verificando a Conexão

Você pode verificar se a conexão com o banco de dados está funcionando corretamente da seguinte forma:

```csharp
bool isConnected = dbAccess.CheckConnection();
if (isConnected)
{
    Console.WriteLine("Conexão bem-sucedida.");
}
else
{
    Console.WriteLine("Falha na conexão.");
}
```

### Executando uma Operação de Modificação de Dados

Para executar uma operação que modifica os dados (por exemplo, INSERT, UPDATE, DELETE), use o método `QueryEdit`:

```csharp
int affectedRows = dbAccess.QueryEdit("SEU_COMANDO_SQL_AQUI");
Console.WriteLine($"{affectedRows} linhas afetadas.");
```

---


## Licença

Este repositório está licenciado sob a [Licença MIT](LICENSE).


---
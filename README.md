# DatabaseAccess
Implementação de classe genérica para acesso a conexão de vários tipos de bancos de dados, utilizando o principio da inversão de dependência com a DbConnection em C#.
Na DLL são implementadas as seguintes classes:
* DbAccess : Abstrai conexões de banco de dados e possue os métodos de execução de queries SQL.
* BuildDb : Classe estática que realiza o chaveamento entre os tipos de conexões.
Foram também implementados alguns testes unitários de validação utilizando banco de dados SQLite.
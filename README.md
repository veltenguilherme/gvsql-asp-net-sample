# GVSQL ASP.NET Sample

Um projeto de exemplo ASP.NET Core Web API demonstrando o uso do **GVSQL**, uma biblioteca ORM de código aberto para PostgreSQL que fornece uma experiência similar ao Entity Framework para aplicações .NET.

## Sobre o GVSQL

**GVSQL** é uma biblioteca de mapeamento objeto-relacional (ORM) de código aberto para PostgreSQL, projetada para simplificar operações de banco de dados em aplicações .NET. Oferece uma API fluente similar ao Entity Framework, facilitando o trabalho com bancos de dados usando modelos fortemente tipados e consultas similares ao LINQ.

🔗 **Repositório**: [https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

## Funcionalidades Demonstradas

Este projeto de exemplo demonstra várias capacidades do GVSQL:

- ✅ **Operações CRUD** - Operações de Criar, Ler, Atualizar e Deletar
- ✅ **Construção de Consultas** - Consultas similares ao LINQ com expressões type-safe
- ✅ **Relacionamentos** - Tratamento de chaves estrangeiras e relacionamentos entre entidades
- ✅ **Suporte a SQL Bruto** - Execute consultas SQL personalizadas quando necessário
- ✅ **Atualizar ou Inserir** - Operações de upsert para sincronização de dados
- ✅ **Injeção de Dependência** - Integração com o container de DI do ASP.NET Core

##  Pré-requisitos

- .NET 8.0 SDK ou superior
- Servidor de banco de dados PostgreSQL
- Visual Studio 2022 ou VS Code (opcional)

## Configuração do Banco de Dados

O projeto está configurado para conectar-se a um banco de dados PostgreSQL. Atualize as configurações de conexão em `DbContext.cs`:

```csharp
private Database CreateDb(string name, int port = 5432, string user = "postgres", string pass = "postgres", string hostName = "127.0.0.1")
```

Configuração padrão:
- **Host**: 127.0.0.1
- **Porta**: 5432
- **Banco de Dados**: gvsql_sample
- **Usuário**: postgres
- **Senha**: postgres

## Como Começar

1. **Clone o repositório**
   ```bash
   git clone <repository-url>
   cd gvsql-asp-net-sample
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Configure seu banco de dados**
   - Certifique-se de que o PostgreSQL está em execução
   - Crie um banco de dados chamado `gvsql_sample` (ou atualize o nome do banco de dados em `DbContext.cs`)
   - Atualize as credenciais de conexão se necessário

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse o Swagger UI**
   - A aplicação será iniciada em `http://localhost:5000`
   - O Swagger UI estará disponível em `http://localhost:5000/swagger`

## Estrutura do Projeto

```
gvsql-asp-net-sample/
├── Controllers/
│   └── SalesController.cs      # Endpoints da API demonstrando operações GVSQL
├── Models/
│   ├── Person.cs               # Modelo de entidade Person
│   ├── User.cs                 # Modelo de entidade User
│   ├── Customer.cs             # Modelo de entidade Customer
│   ├── Partner.cs              # Modelo de entidade Partner
│   └── Sale.cs                 # Modelo de entidade Sale com relacionamentos
├── Tables/
│   ├── Persons.cs              # Classe de tabela para operações Person
│   ├── Users.cs                # Classe de tabela para operações User
│   ├── Customers.cs            # Classe de tabela para operações Customer
│   ├── Partners.cs             # Classe de tabela para operações Partner
│   └── Sales.cs                # Classe de tabela para operações Sale
├── DbContext.cs                 # Configuração do contexto do banco de dados
├── TableMapper.cs              # Mapeamentos de nomes de tabelas
├── Program.cs                  # Ponto de entrada da aplicação
└── Startup.cs                  # Configuração de serviços e middleware
```

## Endpoints da API

O `SalesController` fornece os seguintes endpoints:

### GET `/api/sales/getAll`
Recupera todos os registros de vendas.

### POST `/api/sales/updateOrInsert`
Cria ou atualiza um registro de venda com entidades relacionadas (usuário, cliente, parceiro).

### GET `/api/sales/getByCustomerFirstName?name={name}`
Recupera vendas filtradas pelo primeiro nome do cliente usando consultas similares ao LINQ.

### GET `/api/sales/getByCode?code={code}`
Recupera vendas filtradas por código.

### GET `/api/sales/getByCodeAndNameRawSql?code={code}&name={name}`
Demonstra a execução de consulta SQL bruta com mapeamento de resultado personalizado.

### DELETE `/api/sales/remove?id={guid}`
Remove um registro de venda por GUID.

## Exemplos de Código

### Consulta Básica
```csharp
[HttpGet("getAll")]
public async Task<List<Sale>> GetAll() => await sales.ToListAsync();
```

### Consulta com Filtro
```csharp
[HttpGet("getByCode")]
public async Task<List<Sale>> GetByCode(int code) 
    => await sales.ToListAsync(new Query<Sale>(x => x.Code == code));
```

### Consulta com Relacionamentos
```csharp
[HttpGet("getByCustomerFirstName")]
public async Task<List<Sale>> GetByCustomerFirstName(string name) 
    => await sales.ToListAsync(new Query<Sale>(x => x.Customer.Person.FirstName == name));
```

### Atualizar ou Inserir (Upsert)
```csharp
[HttpPost("updateOrInsert")]
public async Task<Sale> UpdateOrInsert() 
    => await sales.UpdateOrInsertAsync(InsertSale().Result);
```

### Consulta SQL Bruta
```csharp
[HttpGet("getByCodeAndNameRawSql")]
public async Task<List<RawSqlExample>> GetByCodeAndNameRawSql(int code, string name) 
    => await sales.ToListRawAsync<RawSqlExample>($@"
        select *, persons.first_name, persons.last_name, users.nick_name, persons.sex
        from sales
        inner join users on (users.uuid = sales.user_fk)
        inner join persons on (persons.uuid = users.person_fk)
        where code = {code} and persons.first_name = '{name}'");
```

## Exemplo de Definição de Modelo

Modelos no GVSQL usam atributos para definir o esquema do banco de dados:

```csharp
[Table(TableMapper.sales)]
public class Sale : Model<Sale>
{
    [SqlType(SqlTypes.INTEGER_NOT_NULL_UNIQUE)]
    public int Code { get; set; }

    [SqlType(SqlTypes.GUID)]
    [SqlJoin(TableMapper.users)]
    public Guid? UserFk { get; set; }

    [SqlJoin(TableMapper.sales)]
    public User User { get; set; } = new User();
}
```

## Dependências

- **gvsql** (v2.1.15) - A biblioteca ORM GVSQL
- **Swashbuckle.AspNetCore.SwaggerGen** (v6.5.0) - Documentação Swagger/OpenAPI
- **Swashbuckle.AspNetCore.SwaggerUI** (v6.5.0) - Interface Swagger UI

## Contribuindo

Este é um projeto de exemplo. Para contribuições com a biblioteca GVSQL em si, visite:
[https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

## Licença

Este projeto de exemplo é fornecido como está para fins de demonstração. Consulte o repositório GVSQL para informações de licença.

## Recursos

- **Repositório GVSQL**: [https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)
- **Documentação .NET**: [https://docs.microsoft.com/dotnet](https://docs.microsoft.com/dotnet)
- **Documentação PostgreSQL**: [https://www.postgresql.org/docs/](https://www.postgresql.org/docs/)

## Suporte

Para problemas, perguntas ou contribuições relacionadas ao GVSQL, visite o repositório principal:
[https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

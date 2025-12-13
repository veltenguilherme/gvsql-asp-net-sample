# GVSQL ASP.NET Sample

A sample ASP.NET Core Web API project demonstrating the usage of **GVSQL**, an open-source ORM library for PostgreSQL that provides an Entity Framework-like experience for .NET applications.

## About GVSQL

**GVSQL** is an open-source Object-Relational Mapping (ORM) library for PostgreSQL, designed to simplify database operations in .NET applications. It offers a fluent API similar to Entity Framework, making it easy to work with databases using strongly-typed models and LINQ-like queries.

🔗 **Repository**: [https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

## Features Demonstrated

This sample project showcases various GVSQL capabilities:

- ✅ **CRUD Operations** - Create, Read, Update, and Delete operations
- ✅ **Query Building** - LINQ-like queries with type-safe expressions
- ✅ **Relationships** - Handling foreign keys and entity relationships
- ✅ **Raw SQL Support** - Execute custom SQL queries when needed
- ✅ **Update or Insert** - Upsert operations for data synchronization
- ✅ **Dependency Injection** - Integration with ASP.NET Core DI container

## Prerequisites

- .NET 8.0 SDK or later
- PostgreSQL database server
- Visual Studio 2022 or VS Code (optional)

## Database Configuration

The project is configured to connect to a PostgreSQL database. Update the connection settings in `DbContext.cs`:

```csharp
private Database CreateDb(string name, int port = 5432, string user = "postgres", string pass = "postgres", string hostName = "127.0.0.1")
```

Default configuration:
- **Host**: 127.0.0.1
- **Port**: 5432
- **Database**: gvsql_sample
- **User**: postgres
- **Password**: postgres

## Getting Started

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd gvsql-asp-net-sample
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure your database**
   - Ensure PostgreSQL is running
   - Create a database named `gvsql_sample` (or update the database name in `DbContext.cs`)
   - Update connection credentials if needed

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access Swagger UI**
   - The application will start on `http://localhost:5000`
   - Swagger UI will be available at `http://localhost:5000/swagger`

## Project Structure

```
gvsql-asp-net-sample/
├── Controllers/
│   └── SalesController.cs      # API endpoints demonstrating GVSQL operations
├── Models/
│   ├── Person.cs               # Person entity model
│   ├── User.cs                 # User entity model
│   ├── Customer.cs             # Customer entity model
│   ├── Partner.cs              # Partner entity model
│   └── Sale.cs                 # Sale entity model with relationships
├── Tables/
│   ├── Persons.cs              # Table class for Person operations
│   ├── Users.cs                # Table class for User operations
│   ├── Customers.cs            # Table class for Customer operations
│   ├── Partners.cs             # Table class for Partner operations
│   └── Sales.cs                # Table class for Sale operations
├── DbContext.cs                 # Database context configuration
├── TableMapper.cs              # Table name mappings
├── Program.cs                  # Application entry point
└── Startup.cs                  # Service configuration and middleware setup
```

## API Endpoints

The `SalesController` provides the following endpoints:

### GET `/api/sales/getAll`
Retrieves all sales records.

### POST `/api/sales/updateOrInsert`
Creates or updates a sale record with related entities (user, customer, partner).

### GET `/api/sales/getByCustomerFirstName?name={name}`
Retrieves sales filtered by customer's first name using LINQ-like queries.

### GET `/api/sales/getByCode?code={code}`
Retrieves sales filtered by code.

### GET `/api/sales/getByCodeAndNameRawSql?code={code}&name={name}`
Demonstrates raw SQL query execution with custom result mapping.

### DELETE `/api/sales/remove?id={guid}`
Removes a sale record by GUID.

## Code Examples

### Basic Query
```csharp
[HttpGet("getAll")]
public async Task<List<Sale>> GetAll() => await sales.ToListAsync();
```

### Query with Filter
```csharp
[HttpGet("getByCode")]
public async Task<List<Sale>> GetByCode(int code) 
    => await sales.ToListAsync(new Query<Sale>(x => x.Code == code));
```

### Query with Relationships
```csharp
[HttpGet("getByCustomerFirstName")]
public async Task<List<Sale>> GetByCustomerFirstName(string name) 
    => await sales.ToListAsync(new Query<Sale>(x => x.Customer.Person.FirstName == name));
```

### Update or Insert (Upsert)
```csharp
[HttpPost("updateOrInsert")]
public async Task<Sale> UpdateOrInsert() 
    => await sales.UpdateOrInsertAsync(InsertSale().Result);
```

### Raw SQL Query
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

## Model Definition Example

Models in GVSQL use attributes to define database schema:

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

## Dependencies

- **gvsql** (v2.1.15) - The GVSQL ORM library
- **Swashbuckle.AspNetCore.SwaggerGen** (v6.5.0) - Swagger/OpenAPI documentation
- **Swashbuckle.AspNetCore.SwaggerUI** (v6.5.0) - Swagger UI interface

## Contributing

This is a sample project. For contributions to the GVSQL library itself, please visit:
[https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

## License

This sample project is provided as-is for demonstration purposes. Please refer to the GVSQL repository for license information.

## Resources

- **GVSQL Repository**: [https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)
- **.NET Documentation**: [https://docs.microsoft.com/dotnet](https://docs.microsoft.com/dotnet)
- **PostgreSQL Documentation**: [https://www.postgresql.org/docs/](https://www.postgresql.org/docs/)

## Support

For issues, questions, or contributions related to GVSQL, please visit the main repository:
[https://github.com/veltenguilherme/gvsql](https://github.com/veltenguilherme/gvsql)

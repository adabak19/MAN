# Data access
### Introduction of an ORM
The introduction of an ORM (Object-Relational Mapping) like Entity Framework Core has significantly changed how we work with data in our system. An ORM allows us to interact with the database using .NET objects, which abstracts away the underlying SQL queries and provides a more intuitive and type-safe way to work with data. It basically makes it smooth and easy to work with data.
### Using LINQ vs. Traditional SQL
LINQ (Language Integrated Query) is a powerful querying language integrated into C#. It allows us to write queries directly in C# code, which are then translated into SQL by the ORM. This approach is different from the traditional SQL approach, where queries are written as raw SQL strings.
#### Example: Traditional SQL Approach
In the traditional SQL approach, we would write raw SQL queries to interact with the database:
```csharp
using (var connection = new SqlConnection(connectionString))
{
    var command = new SqlCommand("SELECT * FROM Books WHERE Title = @title", connection);
    command.Parameters.AddWithValue("@title", title);
    connection.Open();
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            // Process the data
        }
    }
}
```
#### Example: LINQ Approach
With LINQ, we can write queries directly in C# code, which are then translated into SQL by the ORM:
```csharp
using (var context = new ApplicationDbContext())
{
    var books = context.Books
        .Where(b => b.Title == title)
        .ToList();
}
```
### Migrations and Database Management
Entity Framework Core provides tools for managing database schema changes through migrations. Migrations allow us to evolve the database schema over time while preserving existing data.
To add a new migration, we use the ```dotnet ef migrations add``` command which generates a new migration file that contains the necessary SQL to update the database schema. Then, to apply the migrations to the database, we use the ```dotnet ef database update``` command which applies all pending migrations to the database, ensuring that the schema is up-to-date.
#### Example
If we wanted to add a new property to our Book Model, we would just update it here:
```csharp
public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public int YearPublished { get; set; }
    public int BindingTypeId { get; set; }
    public int PublisherId { get; set; }
    public int AuthorId { get; set; }
    public int Amount { get; set; }
    public string NewColumn { get; set; } // New column
}
```
Then create a migration and update the database - it is that easy!
### Conclusion
The introduction of an ORM like Entity Framework Core has transformed how we work with data in our system. By using LINQ, we can write more intuitive and type-safe queries directly in C# code. Additionally, the use of migrations and ```dotnet ef``` commands allows us to manage database schema changes efficiently. This approach enhances productivity, maintainability, and overall code quality.






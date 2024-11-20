# Building a RESTful Web API with File Storage in .NET

In our recent project, we developed a RESTful web API using .NET to manage various entities such as authors, books, genres, and publishers. Our goal was to create a robust and scalable API that could handle CRUD operations efficiently while using file storage to persist data. In this blog post, we'll walk you through our approach, provide code examples, and share insights into our development process.

## Overview of Our Web API Endpoints

Our web API consists of several endpoints that allow clients to perform CRUD operations on different entities:

### Authors
- **`GET /author`** - Retrieve all authors  
- **`GET /author/{id}`** - Retrieve a specific author by ID  
- **`POST /author`** - Add a new author  
- **`PUT /author/{id}`** - Update an existing author  
- **`DELETE /author/{id}`** - Delete an author  

### Books
- **`GET /book`** - Retrieve all books  
- **`GET /book/{id}`** - Retrieve a specific book by ID  
- **`POST /book`** - Add a new book  
- **`PUT /book/{id}`** - Update an existing book  
- **`DELETE /book/{id}`** - Delete a book  

### Genres
- **`GET /genre`** - Retrieve all genres  
- **`GET /genre/{id}`** - Retrieve a specific genre by ID  
- **`POST /genre`** - Add a new genre  
- **`PUT /genre/{id}`** - Update an existing genre  
- **`DELETE /genre/{id}`** - Delete a genre  

### Publishers
- **`GET /publisher`** - Retrieve all publishers  
- **`GET /publisher/{id}`** - Retrieve a specific publisher by ID  
- **`POST /publisher`** - Add a new publisher  
- **`PUT /publisher/{id}`** - Update an existing publisher  
- **`DELETE /publisher/{id}`** - Delete a publisher  

## Using File Storage to Store Data

To keep our project simple and avoid the overhead of setting up a database, we decided to use file storage to persist our data. Each entity type has its own JSON file where the data is stored. This approach allows us to easily read and write data without the need for a database server.

### File Storage Utility

We created a utility class, `FileStorageUtility`, to handle file operations. This class provides methods to load data from a file and save data to a file asynchronously:

```csharp
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class FileStorageUtility
{
    public static List<T> LoadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public static async Task SaveToFileAsync<T>(string filePath, List<T> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
}
```
### Example: Publisher Service
Here is how we implemented the PublisherService to manage publishers using file storage:
```csharp
using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class PublisherService
    {
        static List<Publisher> Publishers { get; }
        static int nextId;
        static string filePath = "publishers.json";

        static PublisherService()
        {
            Publishers = FileStorageUtility.LoadFromFile<Publisher>(filePath) ?? new List<Publisher>();
            nextId = Publishers.Any() ? Publishers.Max(p => p.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, Publishers);
        }

        public static async Task AddPublisherAsync(Publisher publisher)
        {
            publisher.Id = nextId++;
            Publishers.Add(publisher);
            await SaveToFileAsync();
        }

        public static async Task<List<Publisher>> GetAllAsync()
        {
            return await Task.FromResult(Publishers);
        }

        public static async Task<Publisher?> GetAsync(int id)
        {
            var publisher = Publishers.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(publisher);
        }
    }
}
```
In this service, we use the FileStorageUtility to load and save publishers. The AddPublisherAsync method adds a new publisher to the list and saves the updated list to the file. The GetAllAsync and GetAsync methods retrieve publishers from the list.
## Conclusion
Using file storage for our RESTful web API allowed us to quickly set up data persistence without the need for a database server. This approach is suitable for small projects or prototypes where simplicity and ease of setup are important. By leveraging asynchronous file operations, we ensured that our API remains responsive and efficient.
We hope this overview and code examples provide valuable insights into our project and inspire you to explore similar approaches in your own projects. If you have any questions or feedback, feel free to reach out!

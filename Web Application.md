# Web Application

## Key Requirements Implementation

### Overview

Our web application is built using Blazor WebAssembly for the frontend and ASP.NET Core for the backend. The key requirements are implemented using a combination of Blazor components, services, and API endpoints.

### Key Requirements

1. **User Login and Role Management**:
   - Users can log in to the system using a simple form.
   - JWT (JSON Web Token) is used for authentication.
   - Admins can manage user profiles.

2. **Book Search and Filtering**:
   - Users can search for books by title, author, or genre.

3. **Borrowing and Returning Books**:
   - Users can borrow books directly through the system.
   - Users can view their borrowing history.
   - Admins can view and manage all active loans.

4. **Data Persistence for User and Book Records**:
   - User data is saved securely.
   - Borrowing history is remembered for users.

5. **Admin Dashboard for Inventory Management**:
   - Admins can add, update, and remove books.
   - Admins can monitor user accounts and loans.

### Code Examples

#### Admin Dashboard for Inventory Management
The Admin Dashboard provides administrative functionalities such as managing books and users. It includes cards for active loans, books, and users, and allows navigation to detailed management pages.

**AdminDashboard.razor:**
```razor
@page "/adminDashboard"
@using MAN.Shared.Interfaces
@inject IBookService BookService
@inject IProfileService ProfileService
@inject IBookReadService BookReadService
@inject AuthenticationStateProvider CustomAuthProvider

<h3>Admin Dashboard</h3>

<AuthorizeView Policy="MustBeAdmin">
    <NotAuthorized>
        <p>You must be an admin to access this page.</p>
    </NotAuthorized>
    <Authorized>
        <div class="dashboard-container">
            <div class="dashboard-card">
                <h4>Active Loans</h4>
                <p>@activeLoansCount</p>
            </div>
            <div class="dashboard-card">
                <h4>Books</h4>
                <p>@booksCount</p>
            </div>
            <div class="dashboard-card">
                <h4>Users</h4>
                <p>@usersCount</p>
            </div>
        </div>
    </Authorized>
</AuthorizeView>

@code {
    private int activeLoansCount = 0;
    private int booksCount = 0;
    private int usersCount = 0;
    private bool isLoading = true;
    private bool isAdmin = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var authState = await CustomAuthProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                isAdmin = user.Claims.Any(claim => claim.Type == ClaimTypes.Role && claim.Value == "Admin");

                if (isAdmin)
                {
                    isLoading = true;
                    activeLoansCount = (await BookReadService.GetAllReading()).Count;
                    booksCount = (await BookService.GetAllAsync()).Count;
                    usersCount = (await ProfileService.GetAllAsync()).Count;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing dashboard: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }
}
```
#### Book Search and Filtering

The Book Search and Filtering functionality allows users to search for books by title, author, or genre. 

### Code Example

**BookSearch.razor**:
```razor
@inject IBookService BookService
@using MAN.Shared.Interfaces
@using MAN.Shared.DTO

<div class="search-container">
    <input @bind="searchTitle" @onkeyup="HandleKeyPress" placeholder="Search by Title" />
    <input @bind="searchAuthor" @onkeyup="HandleKeyPress" placeholder="Search by Author" />
    <input @bind="searchGenre" @onkeyup="HandleKeyPress" placeholder="Search by Genre" />
    <button @onclick="PerformSearch">Search</button>
</div>

@code {
    [Parameter] public EventCallback<List<BookDto>> OnSearchResultsChanged { get; set; }
    [Parameter] public bool IsForMyBooks { get; set; }
    [Parameter] public int? ProfileId { get; set; }
    private string? searchTitle;
    private string? searchAuthor;
    private string? searchGenre;

    private async Task PerformSearch()
    {
        if (IsForMyBooks && ProfileId.HasValue)
        {
            var results = await BookService.SearchBooksForUserAsync(ProfileId.Value, searchTitle, searchAuthor, searchGenre);
            await OnSearchResultsChanged.InvokeAsync(results);
        }
        else
        {
            var results = await BookService.SearchBooksAsync(searchTitle, searchAuthor, searchGenre);
            await OnSearchResultsChanged.InvokeAsync(results);
        }
    }

    private async Task HandleKeyPress(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await PerformSearch();
        }
    }
}
```

The `BookService` class provides two distinct search functionalities:

1. **Search for Logged-In User**: The `SearchBooksForUserAsync` method allows logged-in users to search for books specific to their profile. This method takes the user's profile ID and applies the search filters (title, author, genre) to the books associated with that user.

2. **Overall Search for Whole Database**: The `SearchBooksAsync` method allows users to search the entire database of books. This method applies the search filters (title, author, genre) to all books in the database, regardless of the user's profile.

These two methods ensure that users can perform both personalized searches and general searches across the entire book collection.
**BookService.cs**:
```csharp
namespace MAN.Client.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookDto>> SearchBooksAsync(string? title, string? author, string? genre)
        {
            var query = await _httpClient.GetFromJsonAsync<List<BookDto>>("api/book")
                       ?? new List<BookDto>();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.AuthorName.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genres.Any(g => g.Contains(genre, StringComparison.OrdinalIgnoreCase))).ToList();

            return query;
        }

        public async Task<List<BookDto>> SearchBooksForUserAsync(int profileId, string? title, string? author, string? genre)
        {
            var query = await _httpClient.GetFromJsonAsync<List<BookDto>>($"api/book/book/{profileId}")
                       ?? new List<BookDto>();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(b => b.AuthorName.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(b => b.Genres.Any(g => g.Contains(genre, StringComparison.OrdinalIgnoreCase))).ToList();

            return query;
        }
    }
}
```
#### Borrowing and Returning Books

The Borrowing and Returning Books functionality allows users to borrow books directly through the system and return them after reading. Users can also view their borrowing history.

### Code Examples

**SingleBook.razor**:
```razor
private async Task BorrowBook()
    {
        if (profile == null || book == null) return;

        var newBookRead = new BookRead
        {
            ProfileId = profile.Id,
            BookId = book.Id,
            Rating = null,
            Review = null,
            DateStarted = DateOnly.FromDateTime(DateTime.Now),
            DateFinished = null,
            Status = "reading"
        };

        var newBook = new BookDto
        {
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title,
            AuthorName = book.AuthorName,
            Publisher = book.Publisher,
            PageCount = book.PageCount,
            YearPublished = book.YearPublished,
            Genres = book.Genres,
            CoAuthors = book.CoAuthors,
            Amount = book.Amount - 1
        };

        await BookService.Update(newBook);
        await BookReadService.Add(newBookRead);
        book = await BookService.GetAsyncById(Id);
        bookRead = await BookReadService.GetAsyncById(profile.Id, Id);
    }

    private async Task ReturnBook()
    {
        if (profile == null || bookRead == null) return;

        bookRead.DateFinished = DateOnly.FromDateTime(DateTime.Now);
        bookRead.Review = Review;
        bookRead.Rating = Rating;
        bookRead.Status = "read";

        var updatedBook = new BookDto
        {
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title,
            AuthorName = book.AuthorName,
            Publisher = book.Publisher,
            PageCount = book.PageCount,
            YearPublished = book.YearPublished,
            Genres = book.Genres,
            CoAuthors = book.CoAuthors,
            Amount = book.Amount + 1
        };

        await BookService.Update(updatedBook);
        await BookReadService.Update(bookRead);
        book = await BookService.GetAsyncById(Id);
        bookRead = await BookReadService.GetAsyncById(profile.Id, Id);
    }
}
```
## Overview of Pages

### Home Page

The home page provides an overview of the application and links to other pages. It dynamically displays different content based on whether the user is logged in or not.

### Login Page

The login page allows users to log in to the system using their credentials.

### Register Page

The register page allows new users to create an account.

### Books Page

The books page displays a list of books available in the system. Users can search for books by title, author, or genre.

### My Books Page

The My Books page allows users to view the books they have borrowed and their borrowing history. It includes sections for current books, books to read, and books already read.

### Admin Dashboard

The admin dashboard provides administrative functionalities such as managing books and users. It includes cards for active loans, books, and users, and allows navigation to detailed management pages.

### Admin Book Management Page

The Admin Book Management page allows admins to add, update, and remove books from the inventory.

### Admin User Management Page

The Admin User Management page allows admins to manage user accounts, including editing user details and roles.

### Single Book Page

The Single Book page displays detailed information about a specific book, including its title, author, and availability.

### Book Review Page

The Book Review page allows users to view and add reviews for books they have read.

### Book Search Component

The Book Search component is used across various pages to provide a search interface for finding books based on different criteria.

### Frontend and Webservice Connection

### Overview

The frontend Blazor WebAssembly application connects to the backend ASP.NET Core web service using HTTP requests. The `HttpClient` service is used to send requests to the API endpoints. This setup allows the frontend to perform CRUD operations (Create, Read, Update, Delete) and other interactions with the backend services.

The `HttpClient` is configured in the `Program.cs` file and injected into the services that need to communicate with the backend. Each service class, such as `BookService`, `ProfileService`, and `BookReadService`, uses the `HttpClient` to send HTTP requests to the corresponding API endpoints.

The `HttpClient` service handles the following tasks:
- **Sending HTTP Requests**: It sends GET, POST, PUT, and DELETE requests to the API endpoints.
- **Handling Responses**: It processes the responses from the API, including deserializing JSON data into C# objects.
- **Error Handling**: It ensures that any errors during the HTTP requests are properly handled and reported.

### Code Examples

#### HttpClient Configuration

**Program.cs**:
```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp;
using MAN.Shared.Interfaces;
using MAN.Client.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthServiceWEB, JwtAuthService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookReadService, BookReadService>();

await builder.Build().RunAsync();
```
**BookReadService.cs**:
```csharp
namespace MAN.Client.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly HttpClient _httpClient;

        public BookReadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BookRead>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookRead>>("api/bookRead")
                   ?? new List<BookRead>();
        }

        public async Task<BookRead?> GetAsyncById(int profileId, int bookId)
        {
            return await _httpClient.GetFromJsonAsync<BookRead>($"api/bookRead/{profileId}/{bookId}");
        }
```








using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using MAN.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAN.Client.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BookDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookDto>>("api/book")
                   ?? new List<BookDto>();
        }

        public async Task<BookDto?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookDto>($"api/book/{id}");
        }

        public async Task<Book> Add(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync("api/book", book);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Book>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/book/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(BookDto bookDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/book/{bookDto.Id}", bookDto);
            response.EnsureSuccessStatusCode();
        }

// Search Books Implementation
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



    }
}
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
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7216/api/book/{bookDto.Id}", bookDto);
            response.EnsureSuccessStatusCode();
        }


        public async Task<List<BookDto>> SearchBooksForUserAsync(int profileId, string? title, string? author, string? genre)
        {
            // Fetch book reads for the user
            var query = await _httpClient.GetFromJsonAsync<List<BookDto>>($"api/book/book/{profileId}")
                       ?? new List<BookDto>();

            // Apply filters to userBooks
            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query
                    .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query
                    .Where(b => b.AuthorName.Contains(author, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query
                    .Where(b => b.Genres.Any(g => g.Contains(genre, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

            }


            return query;
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
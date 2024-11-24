using LibraryManagement.Shared.Interfaces;
using LibraryManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MAN.Client.Services
{
    public class BookGenreService : IBookGenreService
    {
        private readonly HttpClient _httpClient;

        public BookGenreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BookGenre>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookGenre>>("api/bookGenre")
                   ?? new List<BookGenre>();
        }

        public async Task<BookGenre?> GetAsyncById(int bookId, int genreId)
        {
            return await _httpClient.GetFromJsonAsync<BookGenre>($"api/bookGenre/{bookId}/{genreId}");
        }

        public async Task<BookGenre> Add(BookGenre bookGenre)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookGenre", bookGenre);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookGenre>();
        }

        public async Task Delete(int bookId, int genreId)
        {
            var response = await _httpClient.DeleteAsync($"api/bookGenre/{bookId}/{genreId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(BookGenre bookGenre)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/bookGenre/{bookGenre.BookId}/{bookGenre.GenreId}", bookGenre);
            response.EnsureSuccessStatusCode();
        }
    }
}
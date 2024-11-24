using LibraryManagement.Shared.Interfaces;
using LibraryManagement.Shared.Models;
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
        public async Task<List<Book>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Book>>("api/book")
                   ?? new List<Book>();
        }

        public async Task<Book?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"api/book/{id}");
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

        public async Task Update(Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/book/{book.Id}", book);
            response.EnsureSuccessStatusCode();
        }
    }
}
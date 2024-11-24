using LibraryManagement.Shared.Interfaces;
using LibraryManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAN.Client.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Author>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Author>>("api/author")
                   ?? new List<Author>();
        }

        public async Task<Author?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Author>($"api/author/{id}");
        }

        public async Task<Author> Add(Author author)
        {
            var response = await _httpClient.PostAsJsonAsync("api/author", author);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Author>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/author/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Author author)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/author/{author.Id}", author);
            response.EnsureSuccessStatusCode();
        }
    }
}
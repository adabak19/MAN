using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using MAN.Shared.DTO;
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
        public async Task<List<AuthorDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AuthorDto>>("api/author")
                   ?? new List<AuthorDto>();
        }

        public async Task<AuthorDto?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<AuthorDto>($"api/author/{id}");
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
        public async Task<List<Author>> GetAllAuthors()
        {
            return await _httpClient.GetFromJsonAsync<List<Author>>("api/author")
                   ?? new List<Author>();
        }
    }
}
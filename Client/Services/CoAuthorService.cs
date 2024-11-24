using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MAN.Client.Services
{
    public class CoAuthorService : ICoAuthorService
    {
        private readonly HttpClient _httpClient;

        public CoAuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<CoAuthors>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CoAuthors>>("api/coAuthor")
                   ?? new List<CoAuthors>();
        }

        public async Task<CoAuthors?> GetAsyncById(int bookId, int authorId)
        {
            return await _httpClient.GetFromJsonAsync<CoAuthors>($"api/CoAuthor/{bookId}/{authorId}");
        }

        public async Task<CoAuthors> Add(CoAuthors coAuthor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/coAuthor", coAuthor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CoAuthors>();
        }

        public async Task Delete(int bookId, int authorId)
        {
            var response = await _httpClient.DeleteAsync($"api/coAuthor/{bookId}/{authorId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(CoAuthors coAuthor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/coAuthor/{coAuthor.BookId}/{coAuthor.AuthorId}", coAuthor);
            response.EnsureSuccessStatusCode();
        }
    }
}
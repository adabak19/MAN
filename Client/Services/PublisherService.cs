using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAN.Client.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly HttpClient _httpClient;

        public PublisherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Publisher>>("api/publisher")
                   ?? new List<Publisher>();
        }

        public async Task<Publisher?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Publisher>($"api/publisher/{id}");
        }

        public async Task<Publisher> AddAsync(Publisher publisher)
        {
            var response = await _httpClient.PostAsJsonAsync("api/publisher", publisher);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Publisher>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/publisher/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/publisher/{publisher.Id}", publisher);
            response.EnsureSuccessStatusCode();
        }
    }
}
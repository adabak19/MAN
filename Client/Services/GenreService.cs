using LibraryManagement.Shared.Interfaces;
using LibraryManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAN.Client.Services
{
    public class GenreService : IGenreService
    {
        private readonly HttpClient _httpClient;

        public GenreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Genre>>("api/genre")
                   ?? new List<Genre>();
        }

        public async Task<Genre?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Genre>($"api/genre/{id}");
        }

        public async Task<Genre> Add(Genre genre)
        {
            var response = await _httpClient.PostAsJsonAsync("api/genre", genre);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Genre>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/genre/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(Genre genre)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/genre/{genre.Id}", genre);
            response.EnsureSuccessStatusCode();
        }
    }
}
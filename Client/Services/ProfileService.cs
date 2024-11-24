using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAN.Client.Services
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Profile>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Profile>>("api/profile")
                   ?? new List<Profile>();
        }

        public async Task<Profile?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Profile>($"api/profile/{id}");
        }

        public async Task<Profile> AddAsync(Profile profile)
        {
            var response = await _httpClient.PostAsJsonAsync("api/profile", profile);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Profile>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/profile/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Profile profile)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/profile/{profile.Id}", profile);
            response.EnsureSuccessStatusCode();
        }
    }
}
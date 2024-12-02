using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAN.Shared.DTO;


namespace MAN.Client.Services
{
    public class BookReadService : IBookReadService
    {
        private readonly HttpClient _httpClient;

        public BookReadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BookRead>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookRead>>("api/bookRead")
                   ?? new List<BookRead>();
        }

        public async Task<BookRead?> GetAsyncById(int profileId, int bookId)
        {
            return await _httpClient.GetFromJsonAsync<BookRead>($"api/bookRead/{profileId}/{bookId}");
        }

        public async Task<BookRead> Add(BookRead bookRead)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bookRead", bookRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookRead>();
        }

        public async Task Delete(int profileId, int bookId)
        {
            var response = await _httpClient.DeleteAsync($"api/bookRead/{profileId}/{bookId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(BookRead bookRead)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/bookRead/{bookRead.ProfileId}/{bookRead.BookId}", bookRead);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<BookReadDto>> GetAsyncByBookId(int bookId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<BookReadDto>>($"api/bookRead/{bookId}");
            if (response == null)
            {
                throw new Exception("Failed to fetch book reads.");
            }
            return response;
        }

        public async Task<List<BookReadDto>> GetAsyncByProfileId(int profileId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<BookReadDto>>($"api/bookRead/profile/{profileId}");
            if (response == null)
            {
                throw new Exception("Failed to fetch book reads.");
            }
            return response;
        }
    }
}
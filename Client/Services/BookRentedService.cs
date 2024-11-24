using MAN.Shared.Interfaces;
using MAN.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MAN.Client.Services
{
    public class BookRentedService : IBookRentedService
    {
        private readonly HttpClient _httpClient;

        public BookRentedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BookRented>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookRented>>("api/BookRented")
                   ?? new List<BookRented>();
        }

        public async Task<BookRented?> GetAsyncById(int profileId, int bookId)
        {
            return await _httpClient.GetFromJsonAsync<BookRented>($"api/BookRented/{profileId}/{bookId}");
        }

        public async Task<BookRented> Add(BookRented bookRented)
        {
            var response = await _httpClient.PostAsJsonAsync("api/BookRented", bookRented);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BookRented>();
        }

        public async Task Delete(int profileId, int bookId)
        {
            var response = await _httpClient.DeleteAsync($"api/BookRented/{profileId}/{bookId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(BookRented bookRented)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/BookRented/{bookRented.ProfileId}/{bookRented.BookId}", bookRented);
            response.EnsureSuccessStatusCode();
        }
    }
}
using LibraryManagement.Shared.Interfaces;
using LibraryManagement.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MAN.Client.Services
{
    public class BindingTypeService : IBindingTypeService
    {
        private readonly HttpClient _httpClient;

        public BindingTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BindingType>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BindingType>>("api/bindingType")
                   ?? new List<BindingType>();
        }

        public async Task<BindingType?> GetAsyncById(int id)
        {
            return await _httpClient.GetFromJsonAsync<BindingType>($"api/bindingType/{id}");
        }

        public async Task<BindingType> Add(BindingType bindingType)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bindingType", bindingType);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<BindingType>();
        }

        public async Task Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/bindingType/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task Update(BindingType bindingType)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/bindingType/{bindingType.Id}", bindingType);
            response.EnsureSuccessStatusCode();
        }
    }
}
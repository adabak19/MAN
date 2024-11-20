using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class PublisherService
    {
        static List<Publisher> Publishers { get; }
        static int nextId;
        static string filePath = "publishers.json";

        static PublisherService()
        {
            Publishers = FileStorageUtility.LoadFromFile<Publisher>(filePath) ?? new List<Publisher>();
            nextId = Publishers.Any() ? Publishers.Max(p => p.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, Publishers);
        }

        public static async Task AddPublisherAsync(Publisher publisher)
        {
            publisher.Id = nextId++;
            Publishers.Add(publisher);
            await SaveToFileAsync();
        }

        public static async Task<List<Publisher>> GetAllAsync()
        {
            return await Task.FromResult(Publishers);
        }

        public static async Task<Publisher?> GetAsync(int id)
        {
            var publisher = Publishers.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(publisher);
        }
    }
}
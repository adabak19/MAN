using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class PublisherService
    {
        static List<Publisher> Publishers { get; }
        static int nextId;
        static string filePath = "publishers.json";

        static PublisherService()
        {
            Publishers = LoadFromFile();
            nextId = Publishers.Any() ? Publishers.Max(p => p.Id) + 1 : 1;
        }

        public static List<Publisher> GetAll() => Publishers;

        public static Publisher? Get(int id) => Publishers.FirstOrDefault(p => p.Id == id);

        public static void Add(Publisher publisher)
        {
            publisher.Id = nextId++;
            Publishers.Add(publisher);
            SaveToFile();
        }

        public static void Delete(int id)
        {
            var publisher = Get(id);
            if (publisher is null)
                return;

            Publishers.Remove(publisher);
            SaveToFile();
        }

        public static void Update(Publisher publisher)
        {
            var index = Publishers.FindIndex(p => p.Id == publisher.Id);
            if (index == -1)
                return;

            Publishers[index] = publisher;
            SaveToFile();
        }

        private static List<Publisher> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<Publisher>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Publisher>>(json) ?? new List<Publisher>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Publishers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
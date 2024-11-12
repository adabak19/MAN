using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class GenreService
    {
        static List<Genre> Genres { get; }
        static int nextId;
        static string filePath = "genres.json";

        static GenreService()
        {
            Genres = LoadFromFile();
            nextId = Genres.Any() ? Genres.Max(g => g.Id) + 1 : 1;
        }

        public static List<Genre> GetAll() => Genres;

        public static Genre? Get(int id) => Genres.FirstOrDefault(g => g.Id == id);

        public static void Add(Genre genre)
        {
            genre.Id = nextId++;
            Genres.Add(genre);
            SaveToFile();
        }

        public static void Delete(int id)
        {
            var genre = Get(id);
            if (genre is null)
                return;

            Genres.Remove(genre);
            SaveToFile();
        }

        public static void Update(Genre genre)
        {
            var index = Genres.FindIndex(g => g.Id == genre.Id);
            if (index == -1)
                return;

            Genres[index] = genre;
            SaveToFile();
        }

        private static List<Genre> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<Genre>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Genre>>(json) ?? new List<Genre>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Genres, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class GenreService
    {
        static List<Genre> Genres { get; }
        static int nextId;
        static string filePath = "genres.json";

        static GenreService()
        {
            Genres = FileStorageUtility.LoadFromFile<Genre>(filePath) ?? new List<Genre>();
            nextId = Genres.Any() ? Genres.Max(g => g.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, Genres);
        }

        public static async Task AddGenreAsync(Genre genre)
        {
            genre.Id = nextId++;
            Genres.Add(genre);
            await SaveToFileAsync();
        }

        public static async Task<List<Genre>> GetAllAsync()
        {
            return await Task.FromResult(Genres);
        }

        public static async Task<Genre?> GetAsync(int id)
        {
            var genre = Genres.FirstOrDefault(g => g.Id == id);
            return await Task.FromResult(genre);
        }
    }
}
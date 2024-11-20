using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class ProfileService
    {
        static List<Profile> Profiles { get; }
        static int nextId;
        static string filePath = "profiles.json";

        static ProfileService()
        {
            Profiles = FileStorageUtility.LoadFromFile<Profile>(filePath) ?? new List<Profile>();
            nextId = Profiles.Any() ? Profiles.Max(p => p.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, Profiles);
        }

        public static async Task AddProfileAsync(Profile profile)
        {
            profile.Id = nextId++;
            Profiles.Add(profile);
            await SaveToFileAsync();
        }

        public static async Task<List<Profile>> GetAllAsync()
        {
            return await Task.FromResult(Profiles);
        }

        public static async Task<Profile?> GetAsync(int id)
        {
            var profile = Profiles.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(profile);
        }
    }
}
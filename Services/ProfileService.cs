using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.IO;
using System.Text.Json;

namespace MAN.Services
{
    public static class ProfileService
    {
        static List<Profile> Profiles { get; }
        static int nextId;
        static string filePath = "profiles.json";

        static ProfileService()
        {
            Profiles = LoadFromFile();
            nextId = Profiles.Any() ? Profiles.Max(p => p.Id) + 1 : 1;
        }

        public static List<Profile> GetAll() => Profiles;

        public static Profile? Get(int id) => Profiles.FirstOrDefault(p => p.Id == id);

        public static void Add(Profile profile)
        {
            profile.Id = nextId++;
            Profiles.Add(profile);
            SaveToFile();
        }

        public static void Delete(int id)
        {
            var profile = Get(id);
            if (profile is null)
                return;

            Profiles.Remove(profile);
            SaveToFile();
        }

        public static void Update(Profile profile)
        {
            var index = Profiles.FindIndex(p => p.Id == profile.Id);
            if (index == -1)
                return;

            Profiles[index] = profile;
            SaveToFile();
        }

        private static List<Profile> LoadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new List<Profile>();
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Profile>>(json) ?? new List<Profile>();
        }

        private static void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Profiles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
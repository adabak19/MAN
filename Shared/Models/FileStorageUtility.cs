using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class FileStorageUtility
{
    public static List<T> LoadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public static async Task SaveToFileAsync<T>(string filePath, List<T> data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }
}
using System.Collections.Generic;
using System.Linq;
using MAN.Models;
using System.Threading.Tasks;

namespace MAN.Services
{
    public static class BindingTypeService
    {
        static List<BindingType> BindingTypes { get; }
        static int nextId;
        static string filePath = "bindingtypes.json";

        static BindingTypeService()
        {
            BindingTypes = FileStorageUtility.LoadFromFile<BindingType>(filePath) ?? new List<BindingType>();
            nextId = BindingTypes.Any() ? BindingTypes.Max(bt => bt.Id) + 1 : 1;
        }

        public static async Task SaveToFileAsync()
        {
            await FileStorageUtility.SaveToFileAsync(filePath, BindingTypes);
        }

        public static async Task AddBindingTypeAsync(BindingType bindingType)
        {
            bindingType.Id = nextId++;
            BindingTypes.Add(bindingType);
            await SaveToFileAsync();
        }

        public static async Task<List<BindingType>> GetAllAsync()
        {
            return await Task.FromResult(BindingTypes);
        }

        public static async Task<BindingType?> GetAsync(int id)
        {
            var bindingType = BindingTypes.FirstOrDefault(bt => bt.Id == id);
            return await Task.FromResult(bindingType);
        }
    }
}
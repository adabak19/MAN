using System.Collections.Generic;
using System.Linq;
using MAN.Models;

namespace MAN.Services
{
    public static class BindingTypeService
    {
        static List<BindingType> BindingTypes { get; }
        static int nextId;
        static string filePath = "bindingtypes.json";

        static BindingTypeService()
        {
            BindingTypes = FileStorageUtility.LoadFromFile<BindingType>(filePath);
            nextId = BindingTypes.Any() ? BindingTypes.Max(bt => bt.Id) + 1 : 1;
        }

        public static List<BindingType> GetAll() => BindingTypes;

        public static BindingType? Get(int id) => BindingTypes.FirstOrDefault(bt => bt.Id == id);

        public static void Add(BindingType bindingType)
        {
            bindingType.Id = nextId++;
            BindingTypes.Add(bindingType);
            FileStorageUtility.SaveToFile(filePath, BindingTypes);
        }

        public static void Delete(int id)
        {
            var bindingType = Get(id);
            if (bindingType is null)
                return;

            BindingTypes.Remove(bindingType);
            FileStorageUtility.SaveToFile(filePath, BindingTypes);
        }

        public static void Update(BindingType bindingType)
        {
            var index = BindingTypes.FindIndex(bt => bt.Id == bindingType.Id);
            if (index == -1)
                return;

            BindingTypes[index] = bindingType;
            FileStorageUtility.SaveToFile(filePath, BindingTypes);
        }
    }
}
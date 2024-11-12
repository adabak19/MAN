using System.Collections.Generic;
using MAN.Models;

namespace MAN.Services
{
    public static class BindingTypeService
    {
        static List<BindingType> BindingTypes { get; }
        static int nextId = 1;

        static BindingTypeService()
        {
            BindingTypes = new List<BindingType>
            {
                new BindingType { Id = 1, Type = "Hardcover" },
                new BindingType { Id = 2, Type = "Paperback" },
                new BindingType { Id = 3, Type = "E-book" }
            };
        }

        public static List<BindingType> GetAll() => BindingTypes;

        public static BindingType? Get(int id) => BindingTypes.FirstOrDefault(b => b.Id == id);

        public static void Add(BindingType bindingType)
        {
            bindingType.Id = nextId++;
            BindingTypes.Add(bindingType);
        }

        public static void Delete(int id)
        {
            var bindingType = Get(id);
            if (bindingType is null)
                return;
            BindingTypes.Remove(bindingType);
        }

        public static void Update(BindingType bindingType)
        {
            var index = BindingTypes.FindIndex(b => b.Id == bindingType.Id);
            if (index == -1)
                return;
            BindingTypes[index] = bindingType;
        }
    }
}

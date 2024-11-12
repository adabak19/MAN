using System.Collections.Generic;
using MAN.Models;

namespace MAN.Services
{
    public static class PublisherService
    {
        static List<Publisher> Publishers { get; }
        static int nextId = 1;

        static PublisherService()
        {
            Publishers = new List<Publisher>
            {
                new Publisher { Id = 1, PublisherName = "Penguin Random House" },
                new Publisher { Id = 2, PublisherName = "HarperCollins" },
                new Publisher { Id = 3, PublisherName = "Simon & Schuster" }
            };
        }

        public static List<Publisher> GetAll() => Publishers;

        public static Publisher? Get(int id) => Publishers.FirstOrDefault(p => p.Id == id);

        public static void Add(Publisher publisher)
        {
            publisher.Id = nextId++;
            Publishers.Add(publisher);
        }

        public static void Delete(int id)
        {
            var publisher = Get(id);
            if (publisher is null)
                return;
            Publishers.Remove(publisher);
        }

        public static void Update(Publisher publisher)
        {
            var index = Publishers.FindIndex(p => p.Id == publisher.Id);
            if (index == -1)
                return;
            Publishers[index] = publisher;
        }
    }
}

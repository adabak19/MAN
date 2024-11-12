using System.Collections.Specialized;
using MAN.Models;

namespace MAN.Services;

public static class ProfileService{
    static List<Profile> Profiles {get;}
    static int nextId = 4;
    static ProfileService(){
        Profiles = new List<Profile>
        {
            new Profile { Id = 1, FirstName = "Jan", LastName = "Tolkien", ProfileName = "jpjp"},
            new Profile { Id = 2, FirstName = "Jan", LastName = "Tolkieniak", ProfileName = "jpjpjp"},
            new Profile { Id = 3, FirstName = "John", LastName = "Tolkienowski", ProfileName = "jpjp2"}
        };
    }

    public static List<Profile> GetAll() => Profiles;

    public static Profile? Get(int id) => Profiles.FirstOrDefault(a => a.Id == id);
    public static void Add(Profile profile){
        profile.Id = nextId++;
        Profiles.Add(profile);
    }
    public static void Delete(int id){
        var profile = Get(id);
        if(profile is null)
            return;
        Profiles.Remove(profile);
    }
    public static void Update(Profile profile){
        var index = Profiles.FindIndex(a => a.Id == profile.Id);
        if(index == -1)
            return;
        Profiles[index] = profile;
    }
}

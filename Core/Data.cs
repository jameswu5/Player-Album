using System;

namespace PlayerAlbum;

public static class Data {

    public static string[] CollectionNames = {"Premier League", "Barclays WSL"};

    public static List<Collection> collections;

    static Data() {
        collections = InitialiseCollections();
    }

    private static List<Collection> InitialiseCollections() {
        collections = new List<Collection>();
        foreach (string name in CollectionNames) {
            List<string> clubNames = Database.GetDistinctColumn($"""SELECT DISTINCT Club FROM Player WHERE League = "{name}";""");
            List<Club> clubs = new();
            foreach (string n in clubNames) {
                object[] info = Database.GetColumns($"""SELECT * FROM Club WHERE Name = "{n}";)""")[0];
                clubs.Add(new Club((string)info[0], (string)info[1], (string)info[2]));
            }
            collections.Add(new Collection(name, clubs));
        }
        return collections;
    }

    public static void Initialise() {

    }
}
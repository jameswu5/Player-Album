using System;

namespace PlayerAlbum;

public static class Setup {

    // A list of the active collections in the game
    public static List<Collection> Collections = null!;

    // A dictionary of ID -> Player mappings
    public static Dictionary<long, Player> PlayerMap = null!;

    static Setup() {
        InitialiseCollections();
        InitialisePlayers();
    }

    private static void InitialiseCollections() {
        Collections = new List<Collection>();
        foreach (string name in Constants.CollectionNames) {
            List<string> clubNames = Database.GetDistinctColumn($"""SELECT DISTINCT Club FROM Player WHERE League = "{name}";""");
            List<Club> clubs = new();
            foreach (string n in clubNames) {
                object[] info = Database.GetColumns($"""SELECT * FROM Club WHERE Name = "{n}";)""")[0];
                clubs.Add(new Club((string)info[0], (string)info[1], (string)info[2]));
            }
            Collections.Add(new Collection(name, clubs));
        }
    }

    private static void InitialisePlayers() {
        PlayerMap = new Dictionary<long, Player>();
        foreach (Collection c in Collections) {
            List<Player> players = Database.GetPlayers($"""SELECT * FROM Player WHERE League = "{c.name}";""", initialise: true);
            foreach (Player player in players) {
                PlayerMap[player.ID] = player;
            }
        }
    }

    public static void Initialise() {

    }
}
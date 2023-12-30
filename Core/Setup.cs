using System;

namespace PlayerAlbum;

public static class Setup {

    // A list of the active collections in the game
    public static List<Collection> Collections = null!;

    // A dictionary of ID -> Player mappings
    public static Dictionary<int, Player> PlayerMap = null!;

    // A dictionary of club name -> Club mapppings
    public static Dictionary<string, Club> ClubMap = null!;

    static Setup() {
        InitialiseCollectionsAndClubs();
        InitialisePlayers();
    }

    private static void InitialiseCollectionsAndClubs() {
        Collections = new List<Collection>();
        ClubMap = new Dictionary<string, Club>();
        foreach (string name in Constants.CollectionNames) {
            List<string> clubNames = Database.GetDistinctColumn($"""SELECT DISTINCT Club FROM Player WHERE League = "{name}";""");
            List<Club> clubs = new();
            foreach (string n in clubNames) {
                if (ClubMap.ContainsKey(n)) {
                    clubs.Add(ClubMap[n]);
                } else {
                    object[] info = Database.GetColumns($"""SELECT * FROM Club WHERE Name = "{n}";)""")[0];
                    clubs.Add(new Club((string)info[0], (string)info[1], (string)info[2]));
                }
            }
            Collections.Add(new Collection(name, clubs));
        }
    }

    private static void InitialisePlayers() {
        PlayerMap = new Dictionary<int, Player>();
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
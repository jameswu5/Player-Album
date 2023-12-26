using System;

namespace PlayerAlbum;

public static class Pack {

    public static List<Player> GetRandomPlayers(Collection collection, int count) {
        return Database.GetPlayers($"""SELECT * FROM Player WHERE League = "{collection.name}" ORDER BY Random() LIMIT {count} """);
    }

}
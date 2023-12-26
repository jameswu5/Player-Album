using System;

namespace PlayerAlbum;

public class Action {

    public Game.GameScreen? targetScreen;
    public string? debugText;
    public Collection? collection;
    public Club? club;
    public int? pageShift;
    public Player? player; // Passed for detailed card
    public List<Player>? packedPlayers;

    public Action(Game.GameScreen? targetScreen = null, string? debugText = null, Collection? collection = null, Club? club = null, int? pageShift = null, Player? player = null, List<Player>? packedPlayers = null) {
        this.targetScreen = targetScreen;
        this.debugText = debugText;
        this.collection = collection;
        this.club = club;
        this.pageShift = pageShift;
        this.player = player;
        this.packedPlayers = packedPlayers;
    }
}
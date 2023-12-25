using System;

namespace PlayerAlbum;

public class Action {

    public Game.GameScreen? targetScreen;
    public string? debugText;
    public Collection? collection;
    public Club? club;
    public int? pageShift;

    public Action(Game.GameScreen? targetScreen = null, string? debugText = null, Collection? collection = null, Club? club = null, int? pageShift = null) {
        this.targetScreen = targetScreen;
        this.debugText = debugText;
        this.collection = collection;
        this.club = club;
        this.pageShift = pageShift;
    }
}
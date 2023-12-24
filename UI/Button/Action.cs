using System;

namespace PlayerAlbum;

public class Action {

    public Game.GameScreen? targetScreen;
    public string? debugText;
    public Collection? collection;

    public Action(Game.GameScreen? targetScreen = null, string? debugText = null, Collection? collection = null) {
        this.targetScreen = targetScreen;
        this.debugText = debugText;
        this.collection = collection;
    }

}
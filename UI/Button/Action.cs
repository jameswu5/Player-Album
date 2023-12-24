using System;

namespace PlayerAlbum;

public class Action {

    public Game.GameScreen? targetScreen;
    public string? debugText;

    public Action(Game.GameScreen? targetScreen = null, string? debugText = null) {
        this.targetScreen = targetScreen;
        this.debugText = debugText;
    }

}
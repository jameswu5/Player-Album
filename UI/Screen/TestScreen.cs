using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

/// <summary>
/// This is only for developing purposes and isn't part of the game
/// </summary>
public class TestScreen : Screen {

    private Player TestPlayer = Setup.PlayerMap[226979];

    public TestScreen() {}

    protected override void InitialiseButtons() {}

    public override void Display() {
        DrawRectangle(0, 0, ScreenWidth, ScreenHeight, DarkenFilter);
        TestPlayer.DisplayDetailedCard();
    }
}
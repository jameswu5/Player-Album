using System;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class Game {
    public enum Screen {Home, Menu};
    private Screen currentScreen;

    public Game() {
        currentScreen = Screen.Home;
    }

    public void Run() {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Game");
        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(ScreenColour);
            Update();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    // This is run every frame
    private void Update() {

    }
}
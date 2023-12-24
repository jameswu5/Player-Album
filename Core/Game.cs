using System;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class Game {
    public enum Screen {Home, Menu};
    private Screen currentScreen;
    private Dictionary<Screen, IScreen> screens;

    public Game() {
        Data.Initialise();
        currentScreen = Screen.Home;
        screens = new();
        screens[Screen.Home] = new HomeScreen(Data.collections);
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
        switch (currentScreen) {
            case Screen.Home:
                screens[Screen.Home].Display();
                break;
            case Screen.Menu:
                break;
            default:
                throw new Exception("No screen found.");
        }
    }
}
using System;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class Game {
    public enum GameScreen {Home, Menu};
    private GameScreen currentScreen;
    private Dictionary<GameScreen, Screen> screens;

    public Game() {
        Data.Initialise();
        currentScreen = GameScreen.Home;

        HomeScreen homeScreen = new HomeScreen(Data.collections);
        homeScreen.clickAction += ExecuteAction;

        MenuScreen menuScreen = new MenuScreen();
        menuScreen.clickAction += ExecuteAction;

        screens = new()
        {
            [GameScreen.Home] = homeScreen,
            [GameScreen.Menu] = menuScreen
        };
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
            case GameScreen.Home:
                screens[GameScreen.Home].Display();
                break;
            case GameScreen.Menu:
                screens[GameScreen.Menu].Display();
                break;
            default:
                throw new Exception("No screen found.");
        }
    }

    public void ExecuteAction(Action action) {
        if (action.debugText != null) {
            Console.WriteLine(action.debugText.Length == 0 ? $"Button clicked." : action.debugText);
        }
        if (action.targetScreen != null) {
            currentScreen = (GameScreen)action.targetScreen;
        }
    }
}
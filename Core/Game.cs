using System;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class Game {
    public enum GameScreen {Home, Menu};
    private GameScreen currentScreen;

    private HomeScreen homeScreen;
    private MenuScreen menuScreen;


    public Game() {
        Data.Initialise();
        currentScreen = GameScreen.Home;

        homeScreen = new HomeScreen(Data.collections);
        homeScreen.clickAction += ExecuteAction;

        menuScreen = new MenuScreen();
        menuScreen.clickAction += ExecuteAction;
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
                homeScreen.Display();
                break;
            case GameScreen.Menu:
                menuScreen.Display();
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
            switch (action.targetScreen) {
                case GameScreen.Menu:
                    if (action.collection == null) {
                        throw new Exception("Null collection trying to go to menu screen");
                    }
                    menuScreen.SetClubs((Collection)action.collection); 
                    break;
                default:
                    break;
            }
            currentScreen = (GameScreen)action.targetScreen;
        }
    }
}
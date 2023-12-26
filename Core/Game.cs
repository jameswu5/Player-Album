using System;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class Game {
    public enum GameScreen {Home, Menu, Collection, Pack};
    private GameScreen currentScreen;

    private HomeScreen homeScreen;
    private MenuScreen menuScreen;
    private CollectionScreen collectionScreen;
    private PackScreen packScreen;

    public Game() {
        Data.Initialise();
        currentScreen = GameScreen.Home;

        homeScreen = new HomeScreen(Data.collections);
        homeScreen.clickAction += ExecuteAction;

        menuScreen = new MenuScreen();
        menuScreen.clickAction += ExecuteAction;

        collectionScreen = new CollectionScreen();
        collectionScreen.clickAction += ExecuteAction;

        packScreen = new PackScreen();
        packScreen.clickAction += ExecuteAction;
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
            case GameScreen.Collection:
                collectionScreen.Display();
                break;
            case GameScreen.Pack:
                packScreen.Display();
                break;
            default:
                throw new Exception("No screen found.");
        }
    }

    public void ExecuteAction(Action action) {
        if (action.debugText != null) {
            Console.WriteLine(action.debugText.Length == 0 ? $"Button clicked." : action.debugText);
        }

        // Switch screen
        if (action.targetScreen != null) {
            switch (action.targetScreen) {
                case GameScreen.Menu:
                    if (action.collection != null) {
                        menuScreen.SetClubs((Collection)action.collection); 
                        collectionScreen.collection = (Collection)action.collection;
                    }
                    break;
                case GameScreen.Collection:
                    collectionScreen.SetClub(action.club);
                    break;
                case GameScreen.Pack:
                    // Add logic here

                    break;
                default:
                    break;
            }
            currentScreen = (GameScreen)action.targetScreen;
        }

        // Technically this check is not needed, but it makes the code easier to understand
        if (currentScreen == GameScreen.Collection) {
            if (action.pageShift != null) {
                collectionScreen.ShiftPage((int)action.pageShift);
            }

            collectionScreen.SetDisplayPlayer(action.player);
        }
    }
}
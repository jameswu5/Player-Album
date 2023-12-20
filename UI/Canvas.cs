using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Canvas {
    private static Image img = new("static/badges/MCI.png");

    public static void MainLoop() {
        Raylib.InitWindow(1080, 720, "Game");
        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            GameLoop();
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    public static void GameLoop() {
        Raylib.ClearBackground(Color.BLACK);

        // img.Draw(10, 10, 60, 60);

        // Button button = new(20, 20, 100, 100, "test");
        // button.Render();
        ImageButton imageButton = new (20, 20, 200, 200, img, "Man City");
        imageButton.Render();
    }
}
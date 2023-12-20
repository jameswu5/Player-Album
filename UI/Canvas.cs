using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Canvas {
    private static Image img = new("static/badges/MCI.png", 200, 200);

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
        Raylib.ClearBackground(Color.WHITE);

        HoverButton hoverButton = new(20, 20, 500, 200, text: "new button");
        hoverButton.Render();
        ImageButton imageButton = new(700, 20, img);
        imageButton.Render();
    }
}
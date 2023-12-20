using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Canvas {
    public static void MainLoop() {
        Raylib.InitWindow(1080, 720, "Game");
        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.WHITE);

            Image img = new("static/badges/MCI.png");
            img.Draw(10, 10, 60, 60);

            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }
}
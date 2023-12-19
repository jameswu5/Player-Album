using System;
using Raylib_cs;

public static class Program {
    public static void Main() {
        Loop();
    }

    public static void Loop() {
        Raylib.InitWindow(1080, 720, "Game");
        Raylib.SetTargetFPS(60);
        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();

            Image img = Raylib.LoadImage("static/badges/BUR.png");
            Raylib.ImageResize(ref img, 300, 300);

            Texture2D imge = Raylib.LoadTextureFromImage(img);
            Raylib.DrawTexture(imge, 0, 0, Color.WHITE);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}
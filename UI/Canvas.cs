using System;
using System.Runtime.InteropServices;
using Raylib_cs;

namespace PlayerAlbum;

public class Canvas {

    public static void MainLoop() {
        Raylib.InitWindow(Settings.ScreenWidth, Settings.ScreenHeight, "Game");
        Raylib.SetTargetFPS(60);
        Player player = Database.GetPlayers("Premier League", 1)[0];
        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            // GameLoop();
            
            Raylib.ClearBackground(Color.BLACK);
            player.DisplayDetailedCard();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    public static void GameLoop() {
        Raylib.ClearBackground(Color.WHITE);
    }
}
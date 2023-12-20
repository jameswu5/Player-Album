using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public class Button {
    private static Color colour = Color.WHITE;
    private static Color HoveredColour = Color.YELLOW;

    private readonly int posX;
    private readonly int posY;
    private readonly int height;
    private readonly int width;

    private readonly string name;

    public Button(int posX, int posY, int width, int height, string? name) {
        this.posX = posX;
        this.posY = posY;
        this.height = height;
        this.width = width;
        this.name = name ?? "";
    }

    public void Render() {
        Color buttonColour = colour;

        if (IsHovered(GetMouseX(), GetMouseY())) {
            buttonColour = HoveredColour;
            
            if (IsMouseButtonPressed(0)) {
                Console.WriteLine($"{name} pressed.");
            }
        }

        DrawRectangle(posX, posY, width, height, buttonColour);
    }

    public bool IsHovered(float x, float y) {
        return x >= posX && x <= posX + width && y >= posY && y <= posY + height;
    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public class Button {
    protected readonly int posX;
    protected readonly int posY;
    protected readonly int height;
    protected readonly int width;

    protected readonly string name;

    public Button(int posX, int posY, int width, int height, string? name) {
        this.posX = posX;
        this.posY = posY;
        this.height = height;
        this.width = width;
        this.name = name ?? "";
    }

    public void Render() {
        Display();
        if (IsHovered(GetMouseX(), GetMouseY())) {
            if (IsMouseButtonPressed(0)) {
                OnClick();
            }
        }
    }

    protected virtual void Display() {
        DrawRectangle(posX, posY, width, height, Color.WHITE);
    }

    protected virtual void OnClick() {
        Console.WriteLine($"{name} pressed.");
    }

    protected bool IsHovered(float x, float y) {
        return x >= posX && x <= posX + width && y >= posY && y <= posY + height;
    }
}
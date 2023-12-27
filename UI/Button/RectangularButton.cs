using System;
using Raylib_cs;

namespace PlayerAlbum;

/// <summary>
/// An invisible button with rectangular hitboxes.
/// </summary>
public class RectangularButton : Button {

    protected readonly int posX;
    protected readonly int posY;
    protected readonly int height;
    protected readonly int width;
    protected readonly string name;
    protected readonly string text;

    protected bool activated; // Deactivated buttons still display, but cannot be clicked or hovered

    public RectangularButton(int posX, int posY, int width, int height, string? name = null, string? text = null, Color? textColour = null, int? fontSize = null) : base(name, text, textColour, fontSize) {
        this.posX = posX;
        this.posY = posY;
        this.height = height;
        this.width = width;
    }

    protected override bool IsHovered(float x, float y) => x >= posX && x <= posX + width && y >= posY && y <= posY + height;

    protected override void Display() {}

    protected override void DisplayText() {
        // It should never be null?
        if (text == null) return;
        if (text.Length == 0) return;
        (int x, int y) = Helper.GetTextPositions(text, width, height, fontSize);
        Raylib.DrawText(text, x + posX, y + posY, fontSize, textColour);
    }
}
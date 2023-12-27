using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// An invisible button that displays a border over its hitbox when hovered.
/// </summary>
public class BorderButton : RectangularButton {
    private const int BorderWidth = 4;

    public BorderButton(int posX, int posY, int width, int height, string? name = null) : base(posX, posY, width, height, name) {}

    protected override void HoverDisplay() {
        DrawRectangle(posX - BorderWidth * 2, posY - BorderWidth * 2, width + 4 * BorderWidth, height + 4 * BorderWidth, Color.BLACK);
        DrawRectangle(posX - BorderWidth, posY - BorderWidth, width + 2 * BorderWidth, height + 2 * BorderWidth, Settings.ScreenColour);
    }
}
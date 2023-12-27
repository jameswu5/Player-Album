using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

/// <summary>
/// An invisible button that displays a border over its hitbox when hovered.
/// </summary>
public class BorderButton : RectangularButton {
    public BorderButton(int posX, int posY, int width, int height, string? name = null) : base(posX, posY, width, height, name) {}

    protected override void HoverDisplay() {
        DrawRectangle(posX - ButtonBorderWidth * 2, posY - ButtonBorderWidth * 2, width + 4 * ButtonBorderWidth, height + 4 * ButtonBorderWidth, Color.BLACK);
        DrawRectangle(posX - ButtonBorderWidth, posY - ButtonBorderWidth, width + 2 * ButtonBorderWidth, height + 2 * ButtonBorderWidth, ScreenColour);
    }
}
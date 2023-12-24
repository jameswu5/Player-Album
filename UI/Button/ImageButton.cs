using System;
using System.Reflection.Metadata;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// A button that displays as an image.
/// </summary>
public class ImageButton : Button {
    private const int BorderWidth = 4;
    private Image img;

    public ImageButton(int posX, int posY, Image img, string name = "") : base(posX, posY, img.width, img.height, name) {
        this.img = img;
    }

    protected override void Display() {
        img.Draw(posX, posY);
    }

    protected override void HoverDisplay() {
        DrawRectangle(posX - BorderWidth * 2, posY - BorderWidth * 2, width + 4 * BorderWidth, height + 4 * BorderWidth, Color.BLACK);
        DrawRectangle(posX - BorderWidth, posY - BorderWidth, width + 2 * BorderWidth, height + 2 * BorderWidth, Settings.ScreenColour);
        img.Draw(posX, posY);
    }
}
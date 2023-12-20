using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// A button that displays as an image.
/// </summary>
public class ImageButton : Button {
    
    private Image img;

    public ImageButton(int posX, int posY, Image img, string name = "") : base(posX, posY, img.width, img.height, name) {
        this.img = img;
    }

    protected override void Display() {
        img.Draw(posX, posY);
    }
}
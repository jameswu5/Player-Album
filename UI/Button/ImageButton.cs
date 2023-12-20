using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public class ImageButton : Button {
    
    private Image img;

    public ImageButton(int posX, int posY, int width, int height, Image img, string? name) : base(posX, posY, width, height, name) {
        this.img = img;
    }

    protected override void Display() {
        img.Draw(posX, posY, width, height);
    }
}
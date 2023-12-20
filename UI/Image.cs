using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Image {
    private Raylib_cs.Image img;

    public Image(string path) {
        img = Raylib.LoadImage(path);
    }

    public void Resize(int targetHeight, int targetWidth) {
        Raylib.ImageResize(ref img, targetHeight, targetWidth);
    }

    public void Draw(int posX, int posY, int height, int width) {
        Resize(height, width);
        Texture2D tex = Raylib.LoadTextureFromImage(img);
        Raylib.DrawTexture(tex, posX, posY, Color.WHITE);
    }
}
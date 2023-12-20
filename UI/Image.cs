using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Image {
    private Raylib_cs.Image img;

    public Image(string path) {
        img = Raylib.LoadImage(path);
    }

    public void Resize(int width, int height) {
        Raylib.ImageResize(ref img, width, height);
    }

    public void Draw(int posX, int posY, int width, int height) {
        Resize(width, height);
        Texture2D tex = Raylib.LoadTextureFromImage(img);
        Raylib.DrawTexture(tex, posX, posY, Color.WHITE);
    }
}
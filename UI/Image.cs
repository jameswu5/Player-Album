using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Image {
    private Raylib_cs.Image img;

    public Image(string path) {
        img = Raylib.LoadImage(path);
    }

    public Raylib_cs.Image Resize(int width, int height) {
        Raylib_cs.Image newImg = Raylib.ImageCopy(img);
        Raylib.ImageResize(ref newImg, width, height);
        return newImg;
    }

    public void Draw(int posX, int posY, int width, int height) {
        // I would prefer to load the texture only once, but this seems to work.
        Raylib_cs.Image newImg = Resize(width, height);
        Texture2D tex = Raylib.LoadTextureFromImage(newImg);
        Raylib.DrawTexture(tex, posX, posY, Color.WHITE);
    }
}
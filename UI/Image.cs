using System;
using Raylib_cs;

namespace PlayerAlbum;

public class Image {
    private Raylib_cs.Image img;
    private Texture2D texture;

    public int width;
    public int height;

    public Image(string path, int width, int height) {
        this.width = width;
        this.height = height;
        
        img = Raylib.LoadImage(path);
        Raylib.ImageResize(ref img, width, height);
        texture = Raylib.LoadTextureFromImage(img);
    }

    public void Draw(int posX, int posY) {
        Raylib.DrawTexture(texture, posX, posY, Color.WHITE);
    }
}
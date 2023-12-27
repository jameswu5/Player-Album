using System;
using Raylib_cs;

namespace PlayerAlbum;

public struct Club {
    public string name;
    public string shortcode;
    public Color colour;

    public Club(string name, string shortcode, Color colour) {
        this.name = name;
        this.shortcode = shortcode;
        this.colour = colour;
    }

    public Club(string name, string shortcode, string hexCode) {
        this.name = name;
        this.shortcode = shortcode;
        colour = Helper.ParseHexCode(hexCode);
    }
}
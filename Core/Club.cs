using System;
using Raylib_cs;

namespace PlayerAlbum;

public struct Club {
    public string name;
    public string badgePath;
    public Color colour;

    public Club(string name, string badgePath, Color colour) {
        this.name = name;
        this.badgePath = badgePath;
        this.colour = colour;
    }
}
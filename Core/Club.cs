using System;
using Raylib_cs;

namespace PlayerAlbum;

public struct Club {
    public string name;
    public string shortcode;
    public Color colour;

    public Image menuImage;
    public Image playerImage;

    public Club(string name, string shortcode, string hexCode) {
        this.name = name;
        this.shortcode = shortcode;
        colour = Helper.ParseHexCode(hexCode);

        string imagePath = Helper.GetBadgePath(shortcode);
        menuImage = new Image(imagePath, Settings.MenuScreen.ClubButtonSize, Settings.MenuScreen.ClubButtonSize);
        playerImage = new Image(imagePath, Settings.Player.BadgeSize, Settings.Player.BadgeSize);
    }
}
using System;
using Raylib_cs;

namespace PlayerAlbum;

/// <summary>
/// The default settings.
/// </summary>
public static class Settings {
    public static readonly int ScreenWidth  = 1080;
    public static readonly int ScreenHeight = 720;
    public static readonly Color ScreenColour = new Color(250, 250, 250, 255);

    public static readonly Color ButtonColour = Color.LIGHTGRAY;
    public static readonly Color ButtonHoverColour = Color.DARKGRAY;
    public static readonly Color ButtonTextColour = Color.BLACK;

    public static readonly int SmallFontSize  = 10;
    public static readonly int MediumFontSize = 20;
    public static readonly int LargeFontSize  = 30;

    /* Home Screen UI Settings */
    public static class HomeScreen {
        public static readonly int FontSize = 64;
        public static readonly int ButtonWidth = 600;
        public static readonly int ButtonHeight = 100;
        public static readonly int ButtonFontSize = 36;
        public static readonly int Padding = 40;
    }

    /* Menu Screen UI Settings */
    public static class MenuScreen {
        public static readonly Color HeaderColour = Color.LIGHTGRAY;
        public static readonly int HeaderHeight = 80;
        public static readonly int HeaderFontSize = 30;
    }

    /* Player UI Settings */
    public static class Player {
        // Card icon
        public static readonly int CardWidth = 210;
        public static readonly int CardHeight = 270;
        public static readonly int CardOffset = 12;
        public static readonly int CardFontSize = 20;

        // Detailed Card
        public static readonly int DCardWidth = 810;
        public static readonly int DCardHeight = 540;
        public static readonly int DCardWidthOffset = (ScreenWidth - DCardWidth) >> 1;
        public static readonly int DCardHeightOffset = (ScreenHeight - DCardHeight) >> 1;

        // Left side of detailed card
        public static readonly int DCardImageWidthOffset = 80;
        public static readonly int DCardImageHeightOffset = 90;
    }

}
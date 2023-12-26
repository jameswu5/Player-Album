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
    public static readonly Color DarkenFilter = new Color(0, 0, 0, 150);


    public static readonly Color ButtonColour = Color.LIGHTGRAY;
    public static readonly Color ButtonHoverColour = Color.DARKGRAY;
    public static readonly Color ButtonTextColour = Color.BLACK;

    public static readonly int SmallFontSize  = 10;
    public static readonly int MediumFontSize = 20;
    public static readonly int LargeFontSize  = 30;

    public static readonly Color DefaultDarkTextColour  = Color.BLACK;
    public static readonly Color DefaultLightTextColour = Color.WHITE;
    public static readonly double LuminanceThreshold = 0.5;

    /* Header Settings */
    public static readonly Color HeaderColour = Color.LIGHTGRAY;
    public static readonly int HeaderHeight = 80;
    public static readonly int HeaderFontSize = 30;

    // This is technically not a UI setting so I will likely move it elsewhere
    public static readonly int PlayersPerPack = 5;

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
        // Left side
        public static readonly int ButtonWidth = 400;
        public static readonly int ButtonWidthPadding = (ScreenWidth / 2 - ButtonWidth) / 2;
        public static readonly int ButtonHeight = 250;
        public static readonly int ButtonHeightPadding = (ScreenHeight - HeaderHeight - 2 * ButtonHeight) / 3;

        // Right side
        public static readonly int ClubFontSize = 64;
        public static readonly int ClubTitleBoxHeight = 160;

        public static readonly int ClubsPerRow = 4;
        public static readonly int ClubButtonSize = 64;
        public static readonly int ClubButtonPadding = 35;
        public static readonly int ClubButtonEdgePadding = (ScreenWidth / 2
                                                         - ClubsPerRow * ClubButtonSize
                                                         - (ClubsPerRow - 1) * ClubButtonPadding) / 2;
    }

    /* Player UI Settings */
    public static class Player {
        // Card icon
        public static readonly int CardWidth = 180;
        public static readonly int CardHeight = 240;
        public static readonly int CardOffset = 12;
        public static readonly int CardFontSize = 18;

        // Detailed Card
        public static readonly int DCardWidth = 810;
        public static readonly int DCardHeight = 540;
        public static readonly int DCardWidthOffset = (ScreenWidth - DCardWidth) >> 1;
        public static readonly int DCardHeightOffset = (ScreenHeight - DCardHeight) >> 1;

        // Left side of detailed card
        public static readonly int DCardImageWidthOffset = 80;
        public static readonly int DCardImageHeightOffset = 90;

        // Exit button
        public static readonly int ExitPadding = 20;
        public static readonly int ExitButtonSize = 40;
        public static readonly Color ExitButtonColour = new Color(249,124,124,255);
        public static readonly Color ExitButtonHoverColour = new Color(238,105,105,255);
    }

    /* Collection UI Settings */
    public static class CollectionScreen {
        public static readonly int Rows = 2;
        public static readonly int Columns = 4;

        public static readonly int HorizontalPadding = 100;
        public static readonly int CardPadding = (ScreenWidth - 2 * HorizontalPadding - Columns * Player.CardWidth) / (Columns - 1);
        public static readonly int VerticalPadding = (ScreenHeight - HeaderHeight - Rows * Player.CardHeight) / (Rows + 1);

        public static readonly int DirectionButtonWidth = 50;
        public static readonly int DirectionButtonHeight = 200;
        public static readonly int DirectionButtonPadding = (ScreenHeight - DirectionButtonHeight) >> 1;
        public static readonly int DirectionButtonFontSize = 40;
        public static readonly Color DirectionButtonColour = Color.LIGHTGRAY;

        public static readonly int PageNumberFontSize = 18;
    }

    /* Pack Screen Settings */
    public static class PackScreen {
        public static readonly int TopPadding = 100;

        // These are the same, but I separate them for more customisability
        public static readonly int CardPadding = (ScreenWidth - PlayersPerPack * Player.CardWidth) / (PlayersPerPack + 1);
        public static readonly int SidePadding = CardPadding;

        public static readonly int ButtonWidth = 300;
        public static readonly int ButtonHeight = 80;
        public static readonly int ButtonFontSize = 40;
        public static readonly int ButtonTopPadding = (ScreenHeight - HeaderHeight - TopPadding - Player.CardHeight - ButtonHeight) >> 1;
        public static readonly int ButtonSidePadding = (ScreenWidth - ButtonWidth) >> 1;
    }
}
using System;
using Raylib_cs;
using static PlayerAlbum.Constants;

namespace PlayerAlbum;

/// <summary>
/// The default UI Settings.
/// </summary>
public static class Settings {
    public const int ScreenWidth  = 1080;
    public const int ScreenHeight = 720;
    public static readonly Color ScreenColour = new Color(250, 250, 250, 255);
    public static readonly Color DarkenFilter = new Color(0, 0, 0, 150);

    public static readonly Color ButtonColour = Color.LIGHTGRAY;
    public static readonly Color ButtonHoverColour = Color.DARKGRAY;
    public static readonly Color ButtonTextColour = Color.BLACK;

    public const int ButtonBorderWidth = 4;

    public const int DefaultFontSize = 20;

    public static readonly Color DefaultDarkTextColour  = Color.BLACK;
    public static readonly Color DefaultLightTextColour = Color.WHITE;
    public static readonly double LuminanceThreshold = 0.5;

    /* Header Settings */
    public static readonly Color HeaderColour = Color.LIGHTGRAY;
    public const int HeaderHeight = 80;
    public const int HeaderFontSize = 30;

    /* Home Screen UI Settings */
    public static class HomeScreen {
        public const int FontSize = 64;
        public const int ButtonWidth = 600;
        public const int ButtonHeight = 100;
        public const int ButtonFontSize = 36;
        public const int Padding = 40;
    }

    /* Menu Screen UI Settings */
    public static class MenuScreen {
        // Left side
        public const int ButtonWidth = 400;
        public const int ButtonWidthPadding = (ScreenWidth / 2 - ButtonWidth) / 2;
        public const int ButtonHeight = 250;
        public const int ButtonHeightPadding = (ScreenHeight - HeaderHeight - 2 * ButtonHeight) / 3;

        // Right side
        public const int ClubFontSize = 64;
        public const int ClubTitleBoxHeight = 160;

        public const int ClubsPerRow = 4;
        public const int ClubButtonSize = 64;
        public const int ClubButtonPadding = 35;
        public const int ClubButtonEdgePadding = (ScreenWidth / 2 - ClubsPerRow * ClubButtonSize - (ClubsPerRow - 1) * ClubButtonPadding) / 2;
    }

    /* Player UI Settings */
    public static class Player {
        // Card icon
        public const int CardWidth = 180;
        public const int CardHeight = 240;
        public const int CardOffset = 12;
        public const int CardFontSize = 18;

        // Detailed Card
        public const int DCardWidth = 810;
        public const int DCardHeight = 540;
        public const int BorderWidth = 5;
        public const int DCardImageSize = 250;

        public const int DCardWidthOffset = (ScreenWidth - DCardWidth) >> 1;
        public const int DCardHeightOffset = (ScreenHeight - DCardHeight) >> 1;

        // Left side of detailed card
        public const int DCardImageHeightOffset = 90;
        public const int HeaderStatsFontSize = 50;
        public const int NameFontSize = 32;
        public const int HeaderImageWidthPadding = 10;
        public const int SmallPadding = 5;
        public const int StatsWidthPadding = 15; // padding between stats
        public const int StatsHeightPadding = 10; // padding between the stat name and the corresponding number
        public const int StatsFontSize = 22;

        public const int DCardImageWidthOffset = (DCardWidth / 2 - DCardImageSize) >> 1;
        public const int HeightPadding = (DCardImageHeightOffset - HeaderStatsFontSize) >> 1;
        public const int SegmentHeight = (DCardHeight - DCardImageHeightOffset - DCardImageSize - SmallPadding * 2) / 9;
        
        // Exit button
        public const int ExitPadding = 20;
        public const int ExitButtonSize = 40;
        public static readonly Color ExitButtonColour = new Color(249,124,124,255);
        public static readonly Color ExitButtonHoverColour = new Color(238,105,105,255);
    }

    /* Collection UI Settings */
    public static class CollectionScreen {
        public const int Rows = 2;
        public const int Columns = 4;

        public const int HorizontalPadding = 100;
        public const int CardPadding = (ScreenWidth - 2 * HorizontalPadding - Columns * Player.CardWidth) / (Columns - 1);
        public const int VerticalPadding = (ScreenHeight - HeaderHeight - Rows * Player.CardHeight) / (Rows + 1);

        public const int DirectionButtonWidth = 50;
        public const int DirectionButtonHeight = 200;
        public const int DirectionButtonPadding = (ScreenHeight - DirectionButtonHeight) >> 1;
        public const int DirectionButtonFontSize = 40;
        public static readonly Color DirectionButtonColour = Color.LIGHTGRAY;

        public const int PageNumberFontSize = 18;
    }

    /* Pack Screen Settings */
    public static class PackScreen {
        public const int TopPadding = 100;

        // These are the same, but I separate them for more customisability
        public const int CardPadding = (ScreenWidth - PlayersPerPack * Player.CardWidth) / (PlayersPerPack + 1);
        public const int SidePadding = CardPadding;

        public const int TextBoxHeight = 60;
        public const int TextFontSize = 20;

        public const int ButtonWidth = 300;
        public const int ButtonHeight = 80;
        public const int ButtonFontSize = 40;
        public const int ButtonTopPadding = (ScreenHeight - HeaderHeight - TopPadding - Player.CardHeight - ButtonHeight) >> 1;
        public const int ButtonSidePadding = (ScreenWidth - ButtonWidth) >> 1;
    }
}
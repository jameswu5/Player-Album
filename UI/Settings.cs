using System;
using Raylib_cs;

namespace PlayerAlbum;

/// <summary>
/// The default settings.
/// </summary>
public static class Settings {
    public static readonly int ScreenWidth  = 1080;
    public static readonly int ScreenHeight = 720;

    public static readonly Color ButtonColour = Color.LIGHTGRAY;
    public static readonly Color ButtonHoverColour = Color.DARKGRAY;
    public static readonly Color ButtonTextColour = Color.BLACK;

    public static readonly int SmallFontSize  = 10;
    public static readonly int MediumFontSize = 20;
    public static readonly int LargeFontSize  = 30;

    public static readonly int CardWidth = 210;
    public static readonly int CardHeight = 270;
    public static readonly int CardOffset = 12;
    public static readonly int CardFontSize = 20;

    // DCard - DetailedCard
    public static readonly int DCardWidth = 810;
    public static readonly int DCardHeight = 540;
    public static readonly int DCardWidthOffset = (ScreenWidth - DCardWidth) >> 1;
    public static readonly int DCardHeightOffset = (ScreenHeight - DCardHeight) >> 1;

    // Left side of detailed card
    public static readonly int DCardImageWidthOffset = 80;
    public static readonly int DCardImageHeightOffset = 90;
}
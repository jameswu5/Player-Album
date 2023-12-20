using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// The default settings.
/// </summary>
public static class Settings {
    public static readonly Color ButtonColour = Color.LIGHTGRAY;
    public static readonly Color ButtonHoverColour = Color.DARKGRAY;
    public static readonly Color ButtonTextColour = Color.BLACK;

    public static readonly int SmallFontSize  = 10;
    public static readonly int MediumFontSize = 20;
    public static readonly int LargeFontSize  = 30;

}
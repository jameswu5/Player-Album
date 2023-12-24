using System;
using Raylib_cs;

namespace PlayerAlbum;

public static class Helper {

    public const string BadgePathRoot = "static/badges/";

    public static string GetBadgePath(string shortcode) => $"{BadgePathRoot}{shortcode}.png";

    public static (int, int) GetTextPositions(string text, int width, int height, int fontSize) {
        int textWidth = Raylib.MeasureText(text, fontSize);
        return GetCenteredPositions(textWidth, fontSize, width, height);
    }

    public static (int, int) GetCenteredPositions(int width, int height, int boxWidth, int boxHeight) {
        int x = (boxWidth - width) >> 1;
        int y = (boxHeight - height) >> 1;
        return (x, y);
    }

    public static int[] ParseHexCode(string hexCode) {
        if (hexCode.Length != 6) {
            throw new Exception($"Colour code {hexCode} not of correct length.");
        }

        int[] res = new int[3];
        for (int i = 0; i < 3; i++) {
            res[i] = Convert.ToInt32(hexCode.Substring(i * 2, 2), 16);
        }
        return res;
    }
}
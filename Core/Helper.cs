using System;
using Raylib_cs;

namespace PlayerAlbum;

public static class Helper {

    public const string BadgePathRoot = "static/badges/";

    public static Dictionary<string, string> ClubShortcodes = new() {
        {"AFC Bournemouth", "BOU"},
        {"Arsenal", "ARS"},
        {"Aston Villa", "AVL"},
        {"Brentford", "BRE"},
        {"Brighton", "BRI"},
        {"Bristol City", "BRC"},
        {"Burnley", "BUR"},
        {"Chelsea", "CHE"},
        {"Crystal Palace", "CRY"},
        {"Everton", "EVE"},
        {"Fulham", "FUL"},
        {"Leicester City", "LEI"},
        {"Liverpool", "LIV"},
        {"Luton Town", "LUT"},
        {"Manchester City", "MCI"},
        {"Manchester Utd", "MUN"},
        {"Newcastle Utd", "NEW"},
        {"Nott'm Forest", "NFO"},
        {"Sheffield Utd", "SHU"},
        {"Spurs", "TOT"},
        {"West Ham", "WHU"},
        {"Wolves", "WOL"}
    };

    public static string GetBadgePath(string club) => $"{BadgePathRoot}{ClubShortcodes[club]}.png";

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
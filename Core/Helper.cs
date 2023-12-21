using System;

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
}
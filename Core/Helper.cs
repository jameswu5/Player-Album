using System;
using System.Text;
using Raylib_cs;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public static class Helper {

    public static string GetBadgePath(string shortcode) => $"{Constants.BadgePathRoot}{shortcode}.png";

    public static (int, int) GetTextPositions(string text, int width, int height, int fontSize) {
        int textWidth = Raylib.MeasureText(text, fontSize);
        return GetCenteredPositions(textWidth, fontSize, width, height);
    }

    public static (int, int) GetCenteredPositions(int width, int height, int boxWidth, int boxHeight) {
        int x = (boxWidth - width) >> 1;
        int y = (boxHeight - height) >> 1;
        return (x, y);
    }

    // If the text doesn't fit in the box, split it into two lines
    public static List<(string, int, int)> FitTextInBox(string text, int width, int height, int fontSize) {
        List<(string, int, int)> res = new();
        string[] splitText = text.Split(' ');
        int n = splitText.Length;

        // If it already fits in the box or cannot be split
        if (Raylib.MeasureText(text, fontSize) < width || n == 1) {
            (int x, int y) = GetTextPositions(text, width, height, fontSize);
            res.Add((text, x, y));
            return res;
        }

        int[] prefix = new int[n];
        int[] suffix = new int[n];
        prefix[0] = splitText[0].Length;
        suffix[^1] = splitText[^1].Length;
        for (int i = 1; i < n; i++) {
            prefix[i] = prefix[i-1] + splitText[i].Length;
            suffix[n-i-1] = suffix[n-i] + splitText[n-i-1].Length;
        }

        PriorityQueue<int, int> heap = new();
        for (int i = 0; i < n - 1; i++) {
            int top = prefix[i] + i;
            int bottom = suffix[i+1] + n - 2 - i;
            int score = Math.Abs(top - bottom);
            heap.Enqueue(i, score);
        }

        int splitPoint = heap.Dequeue();
        StringBuilder topSb = new();
        for (int i = 0; i < splitPoint; i++) {
            topSb.Append(splitText[i]);
            topSb.Append(' ');
        }
        topSb.Append(splitText[splitPoint]);
        string topText = topSb.ToString();
        (int x, int y) t = GetTextPositions(topText, width, height / 2, fontSize);
        res.Add((topText, t.x, t.y));

        StringBuilder bottomSb = new();
        for (int i = splitPoint + 1; i < n - 1; i++) {
            bottomSb.Append(splitText[i]);
            bottomSb.Append(' ');
        }
        bottomSb.Append(splitText[^1]);
        string bottomText = bottomSb.ToString();
        (int x, int y) b = GetTextPositions(bottomText, width, height / 2, fontSize);
        res.Add((bottomText, b.x, b.y + height / 2));

        return res;
    }

    public static Color ParseHexCode(string hexCode) {
        if (hexCode.Length != 6) {
            throw new Exception($"Colour code {hexCode} not of correct length.");
        }

        int[] res = new int[3];
        for (int i = 0; i < 3; i++) {
            res[i] = Convert.ToInt32(hexCode.Substring(i * 2, 2), 16);
        }
        return new Color(res[0], res[1], res[2], 255);
    }

    public static double CalculateLuminance(Color colour) => (0.2126 * colour.R + 0.7152 * colour.G + 0.0722 * colour.B) / 255;

    public static Color GetTextColour(Color backgroundColour) {
        return CalculateLuminance(backgroundColour) > LuminanceThreshold ? DefaultDarkTextColour : DefaultLightTextColour;
    }

    public static List<PlayerStatus> GetPlayerStatuses(List<Player> players, Dictionary<int, int> save) {
        List<PlayerStatus> res = new();
        foreach (Player player in players) {
            res.Add(new PlayerStatus(player, save.ContainsKey(player.ID)));
        }
        return res;
    }
}
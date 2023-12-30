using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings.Player;
using System.Numerics;

namespace PlayerAlbum;

public partial class Player {
    private string ImagePath;

    private Image cardImage;
    private Image detailedCardImage;

    private Color clubColour;

    private string[] statTexts;
    private int[] statTextLengths;
    private int[] statTextLengthsPrefix;

    private string statsText1;
    private string statsText2;

    private int[][] hexagonEndpoints;
    private int[][] statCoords;
    private int[][] statTextCentres;

    private List<(string, int, int)> nameText;

    private void InitialiseUI() {
        ImagePath = $"{Constants.FacePathRoot}{ID}.png";
        int size = CardWidth - 4 * CardOffset;
        cardImage = new Image(ImagePath, size, size);
        detailedCardImage = new Image(ImagePath, DCardImageSize, DCardImageSize);
        clubColour = GetColour();

        statTexts = (Position != "GK") ? new string[] {"PAC", "SHO", "PAS", "DRI", "DEF", "PHY"} : new string[] {"DIV", "HAN", "KIC", "REF", "SPE", "POS"};

        statTextLengths = new int[6];
        statTextLengths[0] = MeasureText(statTexts[0], StatsFontSize);
        statTextLengths[1] = MeasureText(statTexts[1], StatsFontSize);
        statTextLengths[2] = MeasureText(statTexts[2], StatsFontSize);
        statTextLengths[3] = MeasureText(statTexts[3], StatsFontSize);
        statTextLengths[4] = MeasureText(statTexts[4], StatsFontSize);
        statTextLengths[5] = MeasureText(statTexts[5], StatsFontSize);

        statTextLengthsPrefix = new int[6];
        statTextLengthsPrefix[0] = statTextLengths[0];
        for (int i = 1; i < 6; i++) {
            statTextLengthsPrefix[i] = statTextLengths[i] + statTextLengthsPrefix[i-1];
        }

        statsText1 = $"{Age} | {Height}cm | {Weight}kg";
        statsText2 = $"{AttackingWorkRate[0]}/{DefensiveWorkRate[0]} | {PreferredFoot[0]} | SM: {SkillMoves} | WF: {WeakFoot}";

        GetHexagonCoords();

        nameText = Helper.FitTextInBox(Name, CardWidth - 2 * CardOffset, CardHeight - CardWidth + CardOffset, CardFontSize);
    }

    // Can make more generalised?
    private Color GetColour() {
        string hexCode = Database.GetDistinctColumn($"""SELECT Colour FROM Club WHERE Name = "{Club}";""")[0];
        return Helper.ParseHexCode(hexCode);
    }

    private void GetHexagonCoords() {
        int[] GetCoordinates(double modulus, double argument) {
            int x = (int)Math.Round(modulus * Math.Cos(argument));
            int y = (int)Math.Round(modulus * Math.Sin(argument));
            return new int[] {x + HexagonCentreX, -y + HexagonCentreY};
        }

        hexagonEndpoints = new int[6][];
        hexagonEndpoints[0] = GetCoordinates(HexagonRadius,  2 * Math.PI / 3);
        hexagonEndpoints[1] = GetCoordinates(HexagonRadius,      Math.PI / 3);
        hexagonEndpoints[2] = GetCoordinates(HexagonRadius,                0);
        hexagonEndpoints[3] = GetCoordinates(HexagonRadius,    - Math.PI / 3);
        hexagonEndpoints[4] = GetCoordinates(HexagonRadius, -2 * Math.PI / 3);
        hexagonEndpoints[5] = GetCoordinates(HexagonRadius,          Math.PI);

        statCoords = new int[6][];
        statCoords[0] = GetCoordinates(HexagonRadius * Pace        / 100.0,  2 * Math.PI / 3);
        statCoords[1] = GetCoordinates(HexagonRadius * Shooting    / 100.0,      Math.PI / 3);
        statCoords[2] = GetCoordinates(HexagonRadius * Passing     / 100.0,                0);
        statCoords[3] = GetCoordinates(HexagonRadius * Dribbling   / 100.0,    - Math.PI / 3);
        statCoords[4] = GetCoordinates(HexagonRadius * Defending   / 100.0, -2 * Math.PI / 3);
        statCoords[5] = GetCoordinates(HexagonRadius * Physicality / 100.0,          Math.PI);

        // Text
        statTextCentres = new int[6][];
        statTextCentres[0] = GetCoordinates(HexagonRadius + HexagonTextPadding,  2 * Math.PI / 3);
        statTextCentres[1] = GetCoordinates(HexagonRadius + HexagonTextPadding,      Math.PI / 3);
        statTextCentres[2] = GetCoordinates(HexagonRadius + HexagonTextPadding,                0);
        statTextCentres[3] = GetCoordinates(HexagonRadius + HexagonTextPadding,    - Math.PI / 3);
        statTextCentres[4] = GetCoordinates(HexagonRadius + HexagonTextPadding, -2 * Math.PI / 3);
        statTextCentres[5] = GetCoordinates(HexagonRadius + HexagonTextPadding,          Math.PI);
    }

    public void DisplayCard(int posX, int posY, bool displayImage) {
        DrawRectangle(posX, posY, CardWidth, CardHeight, clubColour);

        // Main card
        DrawRectangle(
            posX + CardOffset,
            posY + CardOffset,
            CardWidth - 2 * CardOffset,
            CardHeight - 2 * CardOffset,
            Color.WHITE
        );
        
        // Draw the face
        if (displayImage) {
            cardImage.Draw(posX + 2 * CardOffset, posY + 2 * CardOffset);
        }

        // Border around the face
        DrawRectangleLines(
            posX + 2 * CardOffset,
            posY + 2 * CardOffset,
            CardWidth - 4 * CardOffset,
            CardWidth - 4 * CardOffset,
            Color.BLACK
        );

        // Write the name
        foreach ((string t, int x, int y) in nameText) {
            DrawText(t, x + posX + CardOffset, y + posY + CardWidth - 2 * CardOffset, CardFontSize, Settings.DefaultDarkTextColour);
        }
    }

    public void DisplayDetailedCard() {

        // Border
        DrawRectangle(
            DCardWidthOffset - BorderWidth,
            DCardHeightOffset - BorderWidth,
            DCardWidth + BorderWidth * 2,
            DCardHeight + BorderWidth * 2,
            Color.BLACK
        );

        DrawRectangle(
            DCardWidthOffset,
            DCardHeightOffset,
            DCardWidth,
            DCardHeight,
            Settings.ScreenColour
        );

        /* Left hand side */

        // Overall
        DrawText(
            Overall.ToString(),
            DCardWidthOffset + DCardImageWidthOffset + HeaderImageWidthPadding,
            DCardHeightOffset + HeightPadding,
            HeaderStatsFontSize,
            Color.BLACK
        );

        // Position
        DrawText(
            Position,
            DCardWidthOffset + DCardImageWidthOffset + DCardImageSize - MeasureText(Position, HeaderStatsFontSize) - HeaderImageWidthPadding,
            DCardHeightOffset + HeightPadding,
            HeaderStatsFontSize,
            Color.BLACK
        );

        // Face picture
        detailedCardImage.Draw(DCardWidthOffset + DCardImageWidthOffset, DCardHeightOffset + DCardImageHeightOffset);

        DrawRectangleLines(
            DCardWidthOffset + DCardImageWidthOffset,
            DCardHeightOffset + DCardImageHeightOffset,
            DCardImageSize,
            DCardImageSize,
            Color.BLACK
        );

        /* Player information */

        // Player name
        (int namePosX, int namePosY) = Helper.GetTextPositions(Name, DCardWidth >> 1, 3 * SegmentHeight, NameFontSize);
        DrawText(
            Name,
            namePosX + DCardWidthOffset,
            namePosY + DCardHeightOffset + DCardImageHeightOffset + DCardImageSize + SmallPadding,
            NameFontSize,
            Color.BLACK
        );
        
        // Player stats
        DisplayStats();

        // Badges

        // Placeholders - to delete
        DrawRectangle(
            DCardWidthOffset + BadgeEdgePadding,
            Settings.ScreenHeight - DCardHeightOffset - SmallPadding - BadgeHeightPadding - BadgeSize,
            BadgeSize, BadgeSize, Color.YELLOW
        );

        DrawRectangle(
            DCardWidthOffset + BadgeEdgePadding + BadgeSize + BadgePadding,
            Settings.ScreenHeight - DCardHeightOffset - SmallPadding - BadgeHeightPadding - BadgeSize,
            BadgeSize, BadgeSize, Color.YELLOW
        );
        
        /* Middle divider */

        DrawLine(
            DCardWidthOffset + DCardWidth / 2,
            DCardHeightOffset,
            DCardWidthOffset + DCardWidth / 2,
            DCardHeightOffset + DCardHeight,
            Color.BLACK
        );

        /* Right hand side */

        (int x, int y) statsText1Pos = Helper.GetTextPositions(statsText1, DCardWidth >> 1, MainStatsHeight, MainStatsFontSize);
        DrawText(
            statsText1,
            statsText1Pos.x + Settings.ScreenWidth / 2,
            statsText1Pos.y + DCardHeightOffset + HexagonPadding,
            MainStatsFontSize, Settings.DefaultDarkTextColour
        );

        DrawHexagon();

        (int x, int y) statsText2Pos = Helper.GetTextPositions(statsText2, DCardWidth >> 1, MainStatsHeight, MainStatsFontSize);
        DrawText(
            statsText2,
            statsText2Pos.x + Settings.ScreenWidth / 2,
            statsText2Pos.y + Settings.ScreenHeight / 2 + HexagonRadius + SmallPadding,
            MainStatsFontSize, Settings.DefaultDarkTextColour
        );
    }

    private void DisplayStats() {
        int totalStatsWidth = 5 * StatsWidthPadding + statTextLengthsPrefix[5];
        int totalStatsHeight = 2 * StatsFontSize + StatsHeightPadding;

        (int statsOffsetX, int statsOffsetY) = Helper.GetCenteredPositions(totalStatsWidth, totalStatsHeight,
                                               DCardWidth / 2, SegmentHeight * 4);

        int statsPosY = DCardHeightOffset + DCardImageHeightOffset + DCardImageSize + SmallPadding + SegmentHeight * 3 + statsOffsetY;

        DrawText(
            statTexts[0],
            DCardWidthOffset + statsOffsetX,
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[1],
            DCardWidthOffset + statsOffsetX + StatsWidthPadding + statTextLengthsPrefix[0],
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[2],
            DCardWidthOffset + statsOffsetX + StatsWidthPadding * 2 + statTextLengthsPrefix[1],
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[3],
            DCardWidthOffset + statsOffsetX + StatsWidthPadding * 3 + statTextLengthsPrefix[2],
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[4],
            DCardWidthOffset + statsOffsetX + StatsWidthPadding * 4 + statTextLengthsPrefix[3],
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[5],
            DCardWidthOffset + statsOffsetX + StatsWidthPadding * 5 + statTextLengthsPrefix[4],
            statsPosY,
            StatsFontSize,
            Color.BLACK
        );

        int statsNumPosY = statsPosY + StatsFontSize + StatsHeightPadding;

        (int x, int y) pacOffset = Helper.GetTextPositions(Pace.ToString(), statTextLengths[0], StatsFontSize, StatsFontSize);
        DrawText(
            Pace.ToString(),
            DCardWidthOffset + statsOffsetX + pacOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );

        (int x, int y) shoOffset = Helper.GetTextPositions(Shooting.ToString(), statTextLengths[1], StatsFontSize, StatsFontSize);
        DrawText(
            Shooting.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[0] + 1 * StatsWidthPadding + shoOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );

        (int x, int y) pasOffset = Helper.GetTextPositions(Passing.ToString(), statTextLengths[2], StatsFontSize, StatsFontSize);
        DrawText(
            Passing.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[1] + 2 * StatsWidthPadding + pasOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );

        (int x, int y) driOffset = Helper.GetTextPositions(Dribbling.ToString(), statTextLengths[3], StatsFontSize, StatsFontSize);
        DrawText(
            Dribbling.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[2] + 3 * StatsWidthPadding + driOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );

        (int x, int y) defOffset = Helper.GetTextPositions(Defending.ToString(), statTextLengths[4], StatsFontSize, StatsFontSize);
        DrawText(
            Defending.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[3] + 4 * StatsWidthPadding + defOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );

        (int x, int y) phyOffset = Helper.GetTextPositions(Physicality.ToString(), statTextLengths[5], StatsFontSize, StatsFontSize);
        DrawText(
            Physicality.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[4] + 5 * StatsWidthPadding + phyOffset.x,
            statsNumPosY,
            StatsFontSize,
            Color.BLACK
        );
    }

    private void DrawHexagon() {
        DrawCircle(HexagonCentreX, HexagonCentreY, HexagonRadius, Color.BLACK);
        DrawCircle(HexagonCentreX, HexagonCentreY, HexagonRadius - 3, Color.WHITE);

        for (int i = 0; i < 3; i++) {
            DrawLine(hexagonEndpoints[i][0], hexagonEndpoints[i][1], hexagonEndpoints[i+3][0], hexagonEndpoints[i+3][1], Color.BLACK);
        }

        for (int i = 0; i < 6; i++) {
            DrawLineEx(
                new Vector2(statCoords[i][0], statCoords[i][1]),
                new Vector2(statCoords[(i+1)%6][0], statCoords[(i+1)%6][1]),
                3, Color.DARKBLUE
            );
        }
        
        for (int i = 0; i < 6; i++) {
            int posX = statTextCentres[i][0] - MeasureText(statTexts[i], HexagonFontSize) / 2;
            int posY = statTextCentres[i][1] - HexagonFontSize / 2;
            DrawText(statTexts[i], posX, posY, HexagonFontSize, Settings.DefaultDarkTextColour);
        }
    }
}
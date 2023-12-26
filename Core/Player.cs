using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings.Player;
using System.Numerics;

namespace PlayerAlbum;

public class Player {
    public long ID;
    public string Club;
    public string League;
    public string Nation;
    public string Name;
    public string Position;
    public long Age;
    public long Height;
    public long Weight;
    public long Overall;
    public long Pace;
    public long Shooting;
    public long Passing;
    public long Dribbling;
    public long Defending;
    public long Physicality;
    public string DefensiveWorkRate;
    public string AttackingWorkRate;
    public string PreferredFoot;
    public long WeakFoot;
    public long SkillMoves;
    public string Gender;

    public string ImagePath;

    public Image cardImage;
    public Image detailedCardImage;

    public Color clubColour;

    private string[] statTexts;

    public Player(object[] values) {
        ID = (long)values[0];
        Club = (string)values[1];
        League = (string)values[2];
        Nation = (string)values[3];
        Name = (string)values[4];
        Position = (string)values[5];
        Age = (long)values[6];
        Height = (long)values[7];
        Weight = (long)values[8];
        Overall = (long)values[9];
        Pace = (long)values[10];
        Shooting = (long)values[11];
        Passing = (long)values[12];
        Dribbling = (long)values[13];
        Defending = (long)values[14];
        Physicality = (long)values[15];
        DefensiveWorkRate = (string)values[16];
        AttackingWorkRate = (string)values[17];
        PreferredFoot = (string)values[18];
        WeakFoot = (long)values[19];
        SkillMoves = (long)values[20];
        Gender = (string)values[21];

        ImagePath = $"{Constants.FacePathRoot}{ID}.png";
        int size = CardWidth - 4 * CardOffset;
        cardImage = new Image(ImagePath, size, size);
        int detailedSize = DCardWidth / 2 - DCardImageWidthOffset * 2;
        detailedCardImage = new Image(ImagePath, detailedSize, detailedSize);

        clubColour = GetColour();

        statTexts = (Position != "GK") ? new string[] {"PAC", "SHO", "PAS", "DRI", "DEF", "PHY"} : new string[] {"DIV", "HAN", "KIC", "REF", "SPE", "POS"};
    }

    public override string ToString() {
        return $"{ID}: {Name} ({Overall})";
    }

    // Can make more generalised?
    private Color GetColour() {
        string hexCode = Database.GetDistinctColumn($"""SELECT Colour FROM Club WHERE Name = "{Club}";""")[0];
        int[] codes = Helper.ParseHexCode(hexCode);
        return new Color(codes[0], codes[1], codes[2], 255);
    }

    public void DisplayDetailedInfo() {
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"------------ {Name} ------------");
        Console.WriteLine($"ID: {ID}");
        Console.WriteLine($"Club: {Club}");
        Console.WriteLine($"League: {League}");
        Console.WriteLine($"Nation: {Nation}");
        Console.WriteLine($"Position: {Position}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Height: {Height}");
        Console.WriteLine($"Weight: {Weight}");
        Console.WriteLine($"Overall: {Overall}");
        Console.WriteLine($"Pace: {Pace}");
        Console.WriteLine($"Shooting: {Shooting}");
        Console.WriteLine($"Passing: {Passing}");
        Console.WriteLine($"Dribbling: {Dribbling}");
        Console.WriteLine($"Defending: {Defending}");
        Console.WriteLine($"Physicality: {Physicality}");
        Console.WriteLine($"Def Workrate: {DefensiveWorkRate}");
        Console.WriteLine($"Att Workrate: {AttackingWorkRate}");
        Console.WriteLine($"Preferred Foot: {PreferredFoot}");
        Console.WriteLine($"Weak Foot: {WeakFoot}");
        Console.WriteLine($"Skill Moves: {SkillMoves}");
        Console.WriteLine($"Gender: {Gender}");
        Console.WriteLine("------------------------------------------");
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
        // Name might not fit on card -- deal with later
        (int boxX, int boxY) = Helper.GetTextPositions(
            Name,
            CardWidth,
            CardHeight - CardWidth + CardOffset,
            CardFontSize
        );

        int textPosX = boxX + posX;
        int textPosY = boxY +  posY + CardWidth - 2 * CardOffset;

        DrawText(Name, textPosX, textPosY, CardFontSize, Color.BLACK);
    }

    public void DisplayDetailedCard() {

        // Border
        int borderWidth = 5;
        DrawRectangle(
            DCardWidthOffset - borderWidth,
            DCardHeightOffset - borderWidth,
            DCardWidth + borderWidth * 2,
            DCardHeight + borderWidth * 2,
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

        int imageSize = DCardWidth / 2 - DCardImageWidthOffset * 2;

        // Header - heavy maths
        int heightPadding = 20;
        int headerImageWidthPadding = 10;
        int headerFontSize = DCardImageHeightOffset - heightPadding * 2;

        // Overall
        DrawText(
            Overall.ToString(),
            DCardWidthOffset + DCardImageWidthOffset + headerImageWidthPadding,
            DCardHeightOffset + heightPadding,
            headerFontSize,
            Color.BLACK
        );

        // Position
        DrawText(
            Position,
            DCardWidthOffset + DCardImageWidthOffset + imageSize - MeasureText(Position, headerFontSize) - headerImageWidthPadding,
            DCardHeightOffset + heightPadding,
            headerFontSize,
            Color.BLACK
        );


        // Face picture
        detailedCardImage.Draw(DCardWidthOffset + DCardImageWidthOffset, DCardHeightOffset + DCardImageHeightOffset);

        DrawRectangleLines(
            DCardWidthOffset + DCardImageWidthOffset,
            DCardHeightOffset + DCardImageHeightOffset,
            imageSize,
            imageSize,
            Color.BLACK
        );


        // Player information

        int smallPadding = 5;
        int segmentHeight = (DCardHeight - DCardImageHeightOffset - imageSize - heightPadding - smallPadding) / 9;

        // Player name

        // placeholder -- to delete
        // DrawRectangle(
        //     DCardWidthOffset,
        //     DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding,
        //     DCardWidth / 2,
        //     segmentHeight * 3,
        //     Color.LIGHTGRAY
        // );

        int nameFontSize = Math.Min(segmentHeight * 3 - smallPadding, 32);
        (int namePosX, int namePosY) = Helper.GetTextPositions(Name, DCardWidth >> 1, 3 * segmentHeight, nameFontSize);
        DrawText(
            Name,
            namePosX + DCardWidthOffset,
            namePosY + DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding,
            nameFontSize,
            Color.BLACK
        );

        // Stats placeholder -- to delete
        // DrawRectangle(
        //     DCardWidthOffset,
        //     DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 3,
        //     DCardWidth / 2,
        //     segmentHeight * 4,
        //     Color.YELLOW
        // );

        int statsWidthPadding = 15; // padding between stats
        int statsHeightPadding = 10; // padding between the stat name and the corresponding number
        int statsFontSize = 22;

        int[] statTextLengths = new int[6];
        statTextLengths[0] = MeasureText(statTexts[0], statsFontSize);
        statTextLengths[1] = MeasureText(statTexts[1], statsFontSize);
        statTextLengths[2] = MeasureText(statTexts[2], statsFontSize);
        statTextLengths[3] = MeasureText(statTexts[3], statsFontSize);
        statTextLengths[4] = MeasureText(statTexts[4], statsFontSize);
        statTextLengths[5] = MeasureText(statTexts[5], statsFontSize);

        int[] statTextLengthsPrefix = new int[6];
        statTextLengthsPrefix[0] = statTextLengths[0];
        for (int i = 1; i < 6; i++) {
            statTextLengthsPrefix[i] = statTextLengths[i] + statTextLengthsPrefix[i-1];
        }
        int totalStatsWidth = 5 * statsWidthPadding + statTextLengthsPrefix[5];
        int totalStatsHeight = 2 * statsFontSize + statsHeightPadding;

        (int statsOffsetX, int statsOffsetY) = Helper.GetCenteredPositions(totalStatsWidth, totalStatsHeight,
                                               DCardWidth / 2, segmentHeight * 4);

        int statsPosY = DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 3 + statsOffsetY;

        DrawText(
            statTexts[0],
            DCardWidthOffset + statsOffsetX,
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[1],
            DCardWidthOffset + statsOffsetX + statsWidthPadding + statTextLengthsPrefix[0],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[2],
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 2 + statTextLengthsPrefix[1],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[3],
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 3 + statTextLengthsPrefix[2],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[4],
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 4 + statTextLengthsPrefix[3],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            statTexts[5],
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 5 + statTextLengthsPrefix[4],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        int statsNumPosY = statsPosY + statsFontSize + statsHeightPadding;


        (int x, int y) pacOffset = Helper.GetTextPositions(Pace.ToString(), statTextLengths[0], statsFontSize, statsFontSize);

        DrawText(
            Pace.ToString(),
            DCardWidthOffset + statsOffsetX + pacOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );


        (int x, int y) shoOffset = Helper.GetTextPositions(Shooting.ToString(), statTextLengths[1], statsFontSize, statsFontSize);

        DrawText(
            Shooting.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[0] + 1 * statsWidthPadding + shoOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );

        (int x, int y) pasOffset = Helper.GetTextPositions(Passing.ToString(), statTextLengths[2], statsFontSize, statsFontSize);

        DrawText(
            Passing.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[1] + 2 * statsWidthPadding + pasOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );

        (int x, int y) driOffset = Helper.GetTextPositions(Dribbling.ToString(), statTextLengths[3], statsFontSize, statsFontSize);

        DrawText(
            Dribbling.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[2] + 3 * statsWidthPadding + driOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );


        (int x, int y) defOffset = Helper.GetTextPositions(Defending.ToString(), statTextLengths[4], statsFontSize, statsFontSize);

        DrawText(
            Defending.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[3] + 4 * statsWidthPadding + defOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );

        (int x, int y) phyOffset = Helper.GetTextPositions(Physicality.ToString(), statTextLengths[5], statsFontSize, statsFontSize);

        DrawText(
            Physicality.ToString(),
            DCardWidthOffset + statsOffsetX + statTextLengthsPrefix[4] + 5 * statsWidthPadding + phyOffset.x,
            statsNumPosY,
            statsFontSize,
            Color.BLACK
        );


        // Badges placeholder -- to delete
        // DrawRectangle(
        //     DCardWidthOffset,
        //     DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 7,
        //     DCardWidth / 2,
        //     segmentHeight * 2,
        //     Color.SKYBLUE
        // );

        
        /* Middle divider */

        DrawLine(
            DCardWidthOffset + DCardWidth / 2,
            DCardHeightOffset,
            DCardWidthOffset + DCardWidth / 2,
            DCardHeightOffset + DCardHeight,
            Color.BLACK
        );


        /* Right hand side */

        int hexagonRadius = 120;

        DrawHexagon(
            DCardWidthOffset + 3 * DCardWidth / 4,
            DCardHeightOffset + DCardHeight / 2,
            hexagonRadius
        );
    }

    private void DrawHexagon(int centreX, int centreY, int radius) {

        DrawCircle(centreX, centreY, radius, Color.BLACK);
        DrawCircle(centreX, centreY, radius - 3, Color.WHITE);

        int[] GetCoordinates(double modulus, double argument) {
            int x = (int)Math.Round(modulus * Math.Cos(argument));
            int y = (int)Math.Round(modulus * Math.Sin(argument));
            return new int[] {x + centreX, -y + centreY};
        }

        int[][] endpoints = new int[6][];
        endpoints[0] = GetCoordinates(radius,  2 * Math.PI / 3);
        endpoints[1] = GetCoordinates(radius,      Math.PI / 3);
        endpoints[2] = GetCoordinates(radius,                0);
        endpoints[3] = GetCoordinates(radius,    - Math.PI / 3);
        endpoints[4] = GetCoordinates(radius, -2 * Math.PI / 3);
        endpoints[5] = GetCoordinates(radius,          Math.PI);

        for (int i = 0; i < 3; i++) {
            DrawLine(endpoints[i][0], endpoints[i][1], endpoints[i+3][0], endpoints[i+3][1], Color.BLACK);
        }

        int[][] coordinates = new int[6][];
        coordinates[0] = GetCoordinates(radius * Pace        / 100.0,  2 * Math.PI / 3);
        coordinates[1] = GetCoordinates(radius * Shooting    / 100.0,      Math.PI / 3);
        coordinates[2] = GetCoordinates(radius * Passing     / 100.0,                0);
        coordinates[3] = GetCoordinates(radius * Dribbling   / 100.0,    - Math.PI / 3);
        coordinates[4] = GetCoordinates(radius * Defending   / 100.0, -2 * Math.PI / 3);
        coordinates[5] = GetCoordinates(radius * Physicality / 100.0,          Math.PI);

        for (int i = 0; i < 6; i++) {
            DrawLineEx(
                new Vector2(coordinates[i][0], coordinates[i][1]),
                new Vector2(coordinates[(i+1)%6][0], coordinates[(i+1)%6][1]),
                3, Color.DARKBLUE
            );
        }
    }
}
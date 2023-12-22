using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;

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

    public const string ImagePathRoot = "static/faces/";
    public string ImagePath;

    public Image cardImage;
    public Image detailedCardImage;

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

        ImagePath = $"{ImagePathRoot}{ID}.png";
        int size = CardWidth - 4 * CardOffset;
        cardImage = new Image(ImagePath, size, size);
        int detailedSize = DCardWidth / 2 - DCardImageWidthOffset * 2;
        detailedCardImage = new Image(ImagePath, detailedSize, detailedSize);
    }

    public override string ToString() {
        return $"{ID}: {Name} ({Overall})";
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

    public void DisplayCard(int offsetX, int offsetY) {
        // Club border - need to use club colour, using sky blue for now
        DrawRectangle(offsetX, offsetY, CardWidth, CardHeight, Color.SKYBLUE);

        // Main card
        DrawRectangle(
            offsetX + CardOffset,
            offsetY + CardOffset,
            CardWidth - 2 * CardOffset,
            CardHeight - 2 * CardOffset,
            Color.WHITE
        );
        
        // Draw the face
        cardImage.Draw(offsetX + 2 * CardOffset, offsetY + 2 * CardOffset);

        // Border around the face
        DrawRectangleLines(
            offsetX + 2 * CardOffset,
            offsetY + 2 * CardOffset,
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

        int textPosX = boxX + offsetX;
        int textPosY = boxY +  offsetY + CardWidth - 2 * CardOffset;

        DrawText(Name, textPosX, textPosY, CardFontSize, Color.BLACK);
    }

    public void DisplayDetailedCard() {
        DrawRectangle(
            DCardWidthOffset,
            DCardHeightOffset,
            DCardWidth,
            DCardHeight,
            Color.WHITE
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
        DrawRectangle(
            DCardWidthOffset,
            DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding,
            DCardWidth / 2,
            segmentHeight * 3,
            Color.LIGHTGRAY
        );

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
        DrawRectangle(
            DCardWidthOffset,
            DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 3,
            DCardWidth / 2,
            segmentHeight * 4,
            Color.YELLOW
        );

        int statsWidthPadding = 10; // padding between stats
        int statsHeightPadding = 10; // padding between the stat name and the corresponding number
        int statsFontSize = 22;
        int totalStatsWidth = 5 * statsWidthPadding
                            + MeasureText("PAC", statsFontSize)
                            + MeasureText("SHO", statsFontSize)
                            + MeasureText("PAS", statsFontSize)
                            + MeasureText("DRI", statsFontSize)
                            + MeasureText("DEF", statsFontSize)
                            + MeasureText("PHY", statsFontSize);
        int totalStatsHeight = 2 * statsFontSize + statsHeightPadding;

        (int statsOffsetX, int statsOffsetY) = Helper.GetCenteredPositions(totalStatsWidth, totalStatsHeight,
                                               DCardWidth / 2, segmentHeight * 4);

        int statsPosY = DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 3 + statsOffsetY;

        int[] statTextLengths = new int[6];
        statTextLengths[0] = MeasureText("PAC", statsFontSize);
        statTextLengths[1] = MeasureText("SHO", statsFontSize);
        statTextLengths[2] = MeasureText("PAS", statsFontSize);
        statTextLengths[3] = MeasureText("DRI", statsFontSize);
        statTextLengths[4] = MeasureText("DEF", statsFontSize);
        statTextLengths[5] = MeasureText("PHY", statsFontSize);

        int[] statTextLengthsPrefix = new int[6];
        statTextLengthsPrefix[0] = statTextLengths[0];
        for (int i = 1; i < 6; i++) {
            statTextLengthsPrefix[i] = statTextLengths[i] + statTextLengthsPrefix[i-1];
        }

        DrawText(
            "PAC",
            DCardWidthOffset + statsOffsetX,
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            "SHO",
            DCardWidthOffset + statsOffsetX + statsWidthPadding + statTextLengthsPrefix[0],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            "PAS",
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 2 + statTextLengthsPrefix[1],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            "DRI",
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 3 + statTextLengthsPrefix[2],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            "DEF",
            DCardWidthOffset + statsOffsetX + statsWidthPadding * 4 + statTextLengthsPrefix[3],
            statsPosY,
            statsFontSize,
            Color.BLACK
        );

        DrawText(
            "PHY",
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
        DrawRectangle(
            DCardWidthOffset,
            DCardHeightOffset + DCardImageHeightOffset + imageSize + smallPadding + segmentHeight * 7,
            DCardWidth / 2,
            segmentHeight * 2,
            Color.SKYBLUE
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

    }
}
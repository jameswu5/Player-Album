using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

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
        int size = Settings.CardWidth - 4 * Settings.CardOffset;
        cardImage = new(ImagePath, size, size);
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
        DrawRectangle(offsetX, offsetY, Settings.CardWidth, Settings.CardHeight, Color.SKYBLUE);

        // Main card
        DrawRectangle(offsetX + Settings.CardOffset,
                      offsetY + Settings.CardOffset,
                      Settings.CardWidth - 2 * Settings.CardOffset,
                      Settings.CardHeight - 2 * Settings.CardOffset,
                      Color.WHITE);
        
        // Draw the face
        cardImage.Draw(offsetX + 2 * Settings.CardOffset, offsetY + 2 * Settings.CardOffset);

        // Border around the face
        DrawRectangleLines(offsetX + 2 * Settings.CardOffset,
                           offsetY + 2 * Settings.CardOffset,
                           Settings.CardWidth - 4 * Settings.CardOffset,
                           Settings.CardWidth - 4 * Settings.CardOffset,
                           Color.BLACK);

        // Write the name
        // Name might not fit on card -- deal with later
        (int boxX, int boxY) = Helper.GetTextPositions(
            Name,
            Settings.CardWidth,
            Settings.CardHeight - Settings.CardWidth + Settings.CardOffset,
            Settings.CardFontSize
        );

        int textPosX = boxX + offsetX;
        int textPosY = boxY +  offsetY + Settings.CardWidth - 2 * Settings.CardOffset;

        DrawText(Name, textPosX, textPosY, Settings.CardFontSize, Color.BLACK);
    }

    public void DisplayDetailedCard() {
        throw new NotImplementedException();
    }
}
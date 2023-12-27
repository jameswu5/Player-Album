using System;

namespace PlayerAlbum;

public partial class Player {
    public readonly int ID;
    private readonly string Club;
    private readonly string League;
    private readonly string Nation;
    public readonly string Name;
    private readonly string Position;
    private readonly int Age;
    private readonly int Height;
    private readonly int Weight;
    private readonly int Overall;
    private readonly int Pace;
    private readonly int Shooting;
    private readonly int Passing;
    private readonly int Dribbling;
    private readonly int Defending;
    private readonly int Physicality;
    private readonly string DefensiveWorkRate;
    private readonly string AttackingWorkRate;
    private readonly string PreferredFoot;
    private readonly int WeakFoot;
    private readonly int SkillMoves;
    private readonly string Gender;

    public Player(object[] values) {
        ID = (int)(long)values[0];
        Club = (string)values[1];
        League = (string)values[2];
        Nation = (string)values[3];
        Name = (string)values[4];
        Position = (string)values[5];
        Age = (int)(long)values[6];
        Height = (int)(long)values[7];
        Weight = (int)(long)values[8];
        Overall = (int)(long)values[9];
        Pace = (int)(long)values[10];
        Shooting = (int)(long)values[11];
        Passing = (int)(long)values[12];
        Dribbling = (int)(long)values[13];
        Defending = (int)(long)values[14];
        Physicality = (int)(long)values[15];
        AttackingWorkRate = (string)values[16];
        DefensiveWorkRate = (string)values[17];
        PreferredFoot = (string)values[18];
        WeakFoot = (int)(long)values[19];
        SkillMoves = (int)(long)values[20];
        Gender = (string)values[21];
        InitialiseUI();
    }

    public override string ToString() => $"{ID}: {Name} ({Overall})";
}
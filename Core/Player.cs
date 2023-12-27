using System;
using Raylib_cs;
using static PlayerAlbum.Settings.Player;

namespace PlayerAlbum;

public partial class Player {
    public int ID;
    public string Club;
    public string League;
    public string Nation;
    public string Name;
    public string Position;
    public int Age;
    public int Height;
    public int Weight;
    public int Overall;
    public int Pace;
    public int Shooting;
    public int Passing;
    public int Dribbling;
    public int Defending;
    public int Physicality;
    public string DefensiveWorkRate;
    public string AttackingWorkRate;
    public string PreferredFoot;
    public int WeakFoot;
    public int SkillMoves;
    public string Gender;

    private string[] statTexts;

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
        DefensiveWorkRate = (string)values[16];
        AttackingWorkRate = (string)values[17];
        PreferredFoot = (string)values[18];
        WeakFoot = (int)(long)values[19];
        SkillMoves = (int)(long)values[20];
        Gender = (string)values[21];
        statTexts = (Position != "GK") ? new string[] {"PAC", "SHO", "PAS", "DRI", "DEF", "PHY"} : new string[] {"DIV", "HAN", "KIC", "REF", "SPE", "POS"};
        InitialiseUI();
    }

    public override string ToString() => $"{ID}: {Name} ({Overall})";
}
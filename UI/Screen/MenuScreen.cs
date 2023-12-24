using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public class MenuScreen : Screen {

    private List<Club> clubs;

    public MenuScreen() {
        clubs = new List<Club>();
    }

    public void SetClubs(Collection collection) {
        clubs = GetClubs(collection);
    }

    private List<Club> GetClubs(Collection collection) {
        throw new NotImplementedException();
    }

    public override void Display() {

    }
}
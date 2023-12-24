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

    // this is not implemented yet
    private List<Club> GetClubs(Collection collection) {
        // throw new NotImplementedException();
        return new List<Club>();
    }

    public override void Display() {

    }
}
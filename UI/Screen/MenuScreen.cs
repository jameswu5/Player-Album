using System;

namespace PlayerAlbum;

public class MenuScreen : Screen {

    private IEnumerable<Club> clubs;

    public MenuScreen(Collection collection) {
        clubs = GetClubs(collection);
    }

    private IEnumerable<Club> GetClubs(Collection collection) {
        throw new NotImplementedException();
    }

    public override void Display() {
        throw new NotImplementedException();
    }
}
using System;

namespace PlayerAlbum;

public class PackScreen : Screen {

    private IEnumerable<Player> players;

    public PackScreen(IEnumerable<Player> players) {
        this.players = players;
    }

    public override void Display() {
        throw new NotImplementedException();
    }
}
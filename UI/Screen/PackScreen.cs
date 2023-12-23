using System;

namespace PlayerAlbum;

public class PackScreen : IScreen {

    private IEnumerable<Player> players;

    public PackScreen(IEnumerable<Player> players) {
        this.players = players;
    }

    public void Display() {
        throw new NotImplementedException();
    }
}
using System;

namespace PlayerAlbum;

public class CollectionScreen : Screen {

    private IEnumerable<Player> players;

    public CollectionScreen(IEnumerable<Player> players) {
        this.players = players;
    }

    public override void Display() {
        throw new NotImplementedException();
    }
}
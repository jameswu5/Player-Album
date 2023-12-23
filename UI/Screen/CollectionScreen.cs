using System;

namespace PlayerAlbum;

public class CollectionScreen : IScreen {

    private IEnumerable<Player> players;

    public CollectionScreen(IEnumerable<Player> players) {
        this.players = players;
    }

    public void Display() {
        throw new NotImplementedException();
    }
}
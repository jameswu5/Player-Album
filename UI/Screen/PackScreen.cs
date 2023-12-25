using System;

namespace PlayerAlbum;

public class PackScreen : Screen {

    private IEnumerable<Player> players;

    public PackScreen(IEnumerable<Player> players) {
        this.players = players;
    }

    protected override List<Button> InitialiseButtons() {
        throw new NotImplementedException();
    }

    public override void Display() {
        throw new NotImplementedException();
    }
}
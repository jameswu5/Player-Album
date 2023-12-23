using System;
using Raylib_cs;

namespace PlayerAlbum;

public class HomeScreen : IScreen {

    private IEnumerable<Collection> collections;

    public HomeScreen(IEnumerable<Collection> collections) {
        this.collections = collections;
    }

    public void Display() {
        throw new NotImplementedException();
    }
}
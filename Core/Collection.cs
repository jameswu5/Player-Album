using System;

namespace PlayerAlbum;

public struct Collection {
    public string name;
    public string logoPath;

    public Collection(string name, string logoPath) {
        this.name = name;
        this.logoPath = logoPath;
    }
}
using System;

namespace PlayerAlbum;

public struct Collection {
    public string name;
    public List<Club> clubs;

    public Collection(string name, List<Club> clubs) {
        this.name = name;
        this.clubs = clubs;
        clubs.Sort((a, b) => a.shortcode.CompareTo(b.shortcode));
    }
}
using System;

namespace PlayerAlbum;

public struct PlayerStatus {
    public Player player;
    public bool isCollected;

    public PlayerStatus(Player player, bool isCollected) {
        this.player = player;
        this.isCollected = isCollected;
    }
}
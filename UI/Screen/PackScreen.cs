using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.PackScreen;

namespace PlayerAlbum;

public class PackScreen : Screen {

    private List<Player> players;
    private List<Button> playerButtons;
    private List<Button> dynamicButtons;

    public PackScreen() {
        players = new();
        playerButtons = new();
        buttons = InitialiseButtons();
    }

    protected override List<Button> InitialiseButtons() {
        List<Button> res = new();

        // Back button
        TextButton backButton = new TextButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: Color.BLACK,
            text: "Back",
            textColour: Color.WHITE,
            fontSize: HeaderFontSize
        );
        AddButtonAction(backButton, new Action(targetScreen: Game.GameScreen.Menu));
        res.Add(backButton);

        return res;
    }

    public void SetPlayers(List<Player> players) {
        this.players = players;
        dynamicButtons = GetDynamicButtons();
    }

    private List<Button> GetDynamicButtons() {
        List<Button> res = new();

        // OK Button
        TextButton okButton = new TextButton(
            ButtonSidePadding, ScreenHeight - ButtonTopPadding - ButtonHeight, ButtonWidth, ButtonHeight,
            text: "OK", fontSize: ButtonFontSize
        );
        AddButtonAction(okButton, new Action(targetScreen: Game.GameScreen.Menu, packedPlayers: players));
        res.Add(okButton);

        return res;
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        /* Players */
        int playerPosY = HeaderHeight + TopPadding;
        for (int i = 0; i < players.Count; i++) {
            int playerPosX = SidePadding + i * (Settings.Player.CardWidth + CardPadding);
            players[i].DisplayCard(playerPosX, playerPosY);
        }

        /* Buttons */
        foreach (Button button in buttons) {
            button.Render();
        }

        foreach (Button button in dynamicButtons) {
            button.Render();
        }
    }
}
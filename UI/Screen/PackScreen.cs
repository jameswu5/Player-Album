using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;

namespace PlayerAlbum;

public class PackScreen : Screen {

    private List<Player> players;
    private List<Button> playerButtons;

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
        foreach (Player player in players) {
            Console.WriteLine(player.Name);
        }
    }

    private List<Button> GetPlayerButtons() {
        throw new NotImplementedException();
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        /* Buttons */
        foreach (Button button in buttons) {
            button.Render();
        }
    }
}
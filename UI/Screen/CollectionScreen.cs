using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.Player;
using static PlayerAlbum.Settings.CollectionScreen;


namespace PlayerAlbum;

public class CollectionScreen : Screen {

    public int page;
    public List<Player> players;

    public CollectionScreen() {
        page = 0;
        players = new();
        buttons = InitialiseButtons();
    }

    protected override List<Button> InitialiseButtons() {
        List<Button> res = new();

        // Home button
        TextButton backButton = new TextButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: Color.BLACK,
            text: "Back",
            textColour: Color.WHITE,
            fontSize: HeaderFontSize
        );
        AddButtonAction(backButton, new Action(targetScreen: Game.GameScreen.Menu));
        res.Add(backButton);

        // Direction buttons

        TextButton left = new TextButton(
            0, DirectionButtonPadding, DirectionButtonWidth, DirectionButtonHeight,
            colour: DirectionButtonColour, text: "<", fontSize: DirectionButtonFontSize
        );
        AddButtonAction(left, new Action());
        res.Add(left);
        
        TextButton right = new TextButton(
            ScreenWidth - DirectionButtonWidth, DirectionButtonPadding, DirectionButtonWidth, DirectionButtonHeight,
            colour: DirectionButtonColour, text: ">", fontSize: DirectionButtonFontSize
        );
        AddButtonAction(right, new Action());
        res.Add(right);

        return res;
    }

    public void SetClub(Club? c = null) {
        if (c == null) {
            throw new NotImplementedException();
        } else {
            // Club players
            Club club = (Club)c;
            players = Database.GetPlayers($"""SELECT * FROM Player WHERE Club = "{club.name}" ORDER BY Overall DESC""");
        }
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        /* Players */
        int indexOffset = page * Rows * Columns;
        for (int i = 0; i < Rows; i++) {
            for (int j = 0; j < Columns; j++) {
                int index = i * Rows + j + indexOffset;
                int posX = HorizontalPadding + (CardWidth + CardPadding) * j;
                int posY = HeaderHeight + VerticalPadding + (CardHeight + VerticalPadding) * i;
                players[index].DisplayCard(posX, posY);
            }
        }

        /* Buttons */
        foreach (Button button in buttons) {
            button.Render();
        }

    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.MenuScreen;

namespace PlayerAlbum;

public class MenuScreen : Screen {

    private Collection collection;
    private List<Club> clubs;

    private List<Button> buttons;

    public MenuScreen() {
        clubs = new List<Club>();
        buttons = InitialiseButtons();


    }

    private List<Button> InitialiseButtons() {
        List<Button> res = new();

        // Home button
        HoverButton homeButton = new HoverButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: Color.DARKGRAY,
            text: "Home",
            textColour: Color.WHITE,
            fontSize: HeaderFontSize
        );
        AddButtonAction(homeButton, new Action(targetScreen: Game.GameScreen.Home));
        res.Add(homeButton);
        
        // See all player button
        HoverButton allPlayerButton = new HoverButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding, ButtonWidth, ButtonHeight,
            colour: Color.VIOLET,
            text: "See all players",
            fontSize: 40
        );
        AddButtonAction(allPlayerButton, new Action());
        res.Add(allPlayerButton);

        // Open pack button
        HoverButton openPackButton = new HoverButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding * 2 + ButtonHeight, ButtonWidth, ButtonHeight,
            colour: Color.DARKBLUE,
            text: "Open pack",
            fontSize: 40
        );
        AddButtonAction(openPackButton, new Action());
        res.Add(openPackButton);

        return res;
    }

    public void SetClubs(Collection collection) {
        this.collection = collection;
        clubs = collection.clubs;
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        // Collection name
        (int x, int y) headerPos = Helper.GetTextPositions(collection.name, ScreenWidth, HeaderHeight, HeaderFontSize);
        DrawText(collection.name, headerPos.x, headerPos.y, HeaderFontSize, Color.BLACK);


        // Buttons
        foreach (Button button in buttons) {
            button.Render();
        }
    }
}
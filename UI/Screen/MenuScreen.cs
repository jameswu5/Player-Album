using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.MenuScreen;
using System.Runtime.CompilerServices;

namespace PlayerAlbum;

public class MenuScreen : Screen {

    private Collection collection;
    private List<Club> clubs;

    public MenuScreen() {
        clubs = new List<Club>();
        buttons = InitialiseButtons();
    }

    protected override List<Button> InitialiseButtons() {
        List<Button> res = new();

        // Home button
        TextButton homeButton = new TextButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: Color.BLACK,
            text: "Home",
            textColour: Color.WHITE,
            fontSize: HeaderFontSize
        );
        AddButtonAction(homeButton, new Action(targetScreen: Game.GameScreen.Home));
        res.Add(homeButton);
        
        // See all player button
        TextButton allPlayerButton = new TextButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding, ButtonWidth, ButtonHeight,
            colour: Color.SKYBLUE,
            text: "See all players",
            fontSize: 40
        );
        AddButtonAction(allPlayerButton, new Action(targetScreen: Game.GameScreen.Collection));
        res.Add(allPlayerButton);

        // Open pack button
        TextButton openPackButton = new TextButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding * 2 + ButtonHeight, ButtonWidth, ButtonHeight,
            colour: Color.ORANGE,
            text: "Open pack",
            fontSize: 40
        );
        AddButtonAction(openPackButton, new Action());
        res.Add(openPackButton);

        // Right hand side
        int rows = (clubs.Count - 1) / ClubsPerRow + 1;
        int clubRowsNeededHeight = rows * ClubButtonSize + (rows - 1) * ClubButtonPadding;
        int clubTopPadding = (ScreenHeight - HeaderHeight - (ClubFontSize + ClubTitleBoxHeight) / 2 - clubRowsNeededHeight) >> 1;

        for (int i = 0; i < clubs.Count; i++) {
            int r = i / ClubsPerRow;
            int c = i % ClubsPerRow;

            Image img = new Image(Helper.GetBadgePath(clubs[i].shortcode), ClubButtonSize, ClubButtonSize);

            ImageButton button = new(
                ScreenWidth / 2 + ClubButtonEdgePadding + c * (ClubButtonSize + ClubButtonPadding),
                HeaderHeight + (ClubTitleBoxHeight + ClubFontSize) / 2 + clubTopPadding + r * (ClubButtonSize + ClubButtonPadding),
                img, clubs[i].name
            );
            AddButtonAction(button, new Action(targetScreen: Game.GameScreen.Collection, club: clubs[i]));
            res.Add(button);
        }

        return res;
    }

    public void SetClubs(Collection collection) {
        this.collection = collection;
        clubs = collection.clubs;
        buttons = InitialiseButtons();
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        // Collection name
        (int x, int y) headerPos = Helper.GetTextPositions(collection.name, ScreenWidth, HeaderHeight, HeaderFontSize);
        DrawText(collection.name, headerPos.x, headerPos.y, HeaderFontSize, Color.BLACK);

        // Club title text
        string clubText = "CLUBS";

        (int x, int y) clubTextPos = Helper.GetTextPositions(clubText, ScreenWidth / 2, ClubTitleBoxHeight, ClubFontSize);
        DrawText(clubText, ScreenWidth / 2 + clubTextPos.x, HeaderHeight + clubTextPos.y, ClubFontSize, Color.BLACK);

        // Buttons
        foreach (Button button in buttons) {
            button.Render();
        }
    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.MenuScreen;

namespace PlayerAlbum;

public class MenuScreen : Screen {

    private Collection collection;
    private List<Club> clubs;

    public MenuScreen() {
        clubs = new List<Club>();
    }

    protected override void InitialiseButtons() {
        staticButtons = new();

        // Home button
        HoverButton homeButton = new HoverButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: HomeButtonColour,
            hoverColour: HomeButtonHoverColour,
            text: "Home",
            textColour: Helper.GetTextColour(HomeButtonColour),
            fontSize: HeaderFontSize
        );
        AddButtonAction(homeButton, new Action(targetScreen: Game.GameScreen.Home));
        staticButtons.Add(homeButton);
        
        // See all player button
        HoverButton allPlayerButton = new HoverButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding, ButtonWidth, ButtonHeight,
            colour: Palette.LightBlue,
            hoverColour: Palette.DarkBlue,
            text: "See all players",
            fontSize: 40
        );
        AddButtonAction(allPlayerButton, new Action(targetScreen: Game.GameScreen.Collection));
        staticButtons.Add(allPlayerButton);

        // Open pack button
        HoverButton openPackButton = new HoverButton(
            ButtonWidthPadding, HeaderHeight + ButtonHeightPadding * 2 + ButtonHeight, ButtonWidth, ButtonHeight,
            colour: Palette.LightGreen,
            hoverColour: Palette.DarkGreen,
            text: "Open pack",
            fontSize: 40
        );
        AddButtonAction(openPackButton, new Action(targetScreen: Game.GameScreen.Pack));
        staticButtons.Add(openPackButton);

        // Right hand side
        int rows = (clubs.Count - 1) / ClubsPerRow + 1;
        int clubRowsNeededHeight = rows * ClubButtonSize + (rows - 1) * ClubButtonPadding;
        int clubTopPadding = (ScreenHeight - HeaderHeight - (ClubFontSize + ClubTitleBoxHeight) / 2 - clubRowsNeededHeight) >> 1;

        for (int i = 0; i < clubs.Count; i++) {
            int r = i / ClubsPerRow;
            int c = i % ClubsPerRow;

            int posX = ScreenWidth / 2 + ClubButtonEdgePadding + c * (ClubButtonSize + ClubButtonPadding);
            int posY = HeaderHeight + (ClubTitleBoxHeight + ClubFontSize) / 2 + clubTopPadding + r * (ClubButtonSize + ClubButtonPadding);

            BorderButton button = new BorderButton(posX, posY, ClubButtonSize, ClubButtonSize, clubs[i].name);
            
            AddButtonAction(button, new Action(targetScreen: Game.GameScreen.Collection, club: clubs[i]));
            staticButtons.Add(button);
        }
    }

    public void SetClubs(Collection collection) {
        this.collection = collection;
        clubs = collection.clubs;
        InitialiseButtons();
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);

        // Collection name
        (int x, int y) headerPos = Helper.GetTextPositions(collection.name, ScreenWidth, HeaderHeight, HeaderFontSize);
        DrawText(collection.name, headerPos.x, headerPos.y, HeaderFontSize, DefaultDarkTextColour);

        // Club title text
        string clubText = "CLUBS";

        (int x, int y) clubTextPos = Helper.GetTextPositions(clubText, ScreenWidth / 2, ClubTitleBoxHeight, ClubFontSize);
        DrawText(clubText, ScreenWidth / 2 + clubTextPos.x, HeaderHeight + clubTextPos.y, ClubFontSize, DefaultDarkTextColour);

        // Buttons
        foreach (Button button in staticButtons) {
            button.Render();
        }
        
        // Club images
        int rows = (clubs.Count - 1) / ClubsPerRow + 1;
        int clubRowsNeededHeight = rows * ClubButtonSize + (rows - 1) * ClubButtonPadding;
        int clubTopPadding = (ScreenHeight - HeaderHeight - (ClubFontSize + ClubTitleBoxHeight) / 2 - clubRowsNeededHeight) >> 1;

        for (int i = 0; i < clubs.Count; i++) {
            int r = i / ClubsPerRow;
            int c = i % ClubsPerRow;

            int posX = ScreenWidth / 2 + ClubButtonEdgePadding + c * (ClubButtonSize + ClubButtonPadding);
            int posY = HeaderHeight + (ClubTitleBoxHeight + ClubFontSize) / 2 + clubTopPadding + r * (ClubButtonSize + ClubButtonPadding);

            clubs[i].menuImage.Draw(posX, posY);
        }
    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.Player;
using static PlayerAlbum.Settings.PackScreen;

namespace PlayerAlbum;

public class PackScreen : Screen {

    private List<PlayerStatus> playerStatuses;
    private List<Button> dynamicButtons;
    private HoverButton exitButton;

    private Player? displayPlayer;

    private const string OwnedText = "Owned";

    public PackScreen() {
        playerStatuses = new();
        dynamicButtons = new();
        staticButtons = new();
        InitialiseButtons();
    }

    protected override void InitialiseButtons() {
        staticButtons = new();

        // Exit button
        exitButton = new HoverButton(
            DCardWidthOffset + DCardWidth - ExitPadding - ExitButtonSize,
            DCardHeightOffset + ExitPadding,
            ExitButtonSize, ExitButtonSize, colour: ExitButtonColour, hoverColour: ExitButtonHoverColour, text: "x"
        );
        AddButtonAction(exitButton, new Action(player: null));
    }

    public void SetPlayers(List<PlayerStatus> playerStatuses) {
        this.playerStatuses = playerStatuses;
        dynamicButtons = GetDynamicButtons();
    }

    private List<Button> GetDynamicButtons() {
        List<Button> res = new();

        // OK Button
        HoverButton okButton = new HoverButton(
            ButtonSidePadding, ScreenHeight - ButtonTopPadding - ButtonHeight, ButtonWidth, ButtonHeight,
            text: "OK", fontSize: ButtonFontSize
        );

        List<Player> players = new();
        foreach (PlayerStatus playerStatus in playerStatuses) {
            players.Add(playerStatus.player);
        }
        AddButtonAction(okButton, new Action(targetScreen: Game.GameScreen.Menu, packedPlayers: players));
        res.Add(okButton);

        // Player Button
        int posY = HeaderHeight + TopPadding;
        for (int i = 0; i < playerStatuses.Count; i++) {
            int posX = SidePadding + i * (CardWidth + CardPadding);
            BorderButton button = new BorderButton(posX, posY, CardWidth, CardHeight);
            AddButtonAction(button, new Action(player: players[i]));
            res.Add(button);
        }

        return res;
    }

    public override void Display() {
        /* Header */
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, HeaderColour);
        string title = "Pack";
        (int x, int y) headerPos = Helper.GetTextPositions(title, ScreenWidth, HeaderHeight, HeaderFontSize);
        DrawText(title, headerPos.x, headerPos.y, HeaderFontSize, DefaultDarkTextColour);

        /* Players */
        foreach (Button button in dynamicButtons) {
            button.Render();
        }

        int playerPosY = HeaderHeight + TopPadding;
        for (int i = 0; i < playerStatuses.Count; i++) {
            int playerPosX = SidePadding + i * (CardWidth + CardPadding);
            playerStatuses[i].player.DisplayCard(playerPosX, playerPosY, true);

            // If they're already owned, display that fact
            if (playerStatuses[i].isCollected) {
                (int x, int y) textPos = Helper.GetTextPositions(OwnedText, CardWidth, TextBoxHeight, TextFontSize);
                DrawText(
                    OwnedText,
                    playerPosX + textPos.x,
                    playerPosY + CardHeight + textPos.y,
                    TextFontSize,
                    DefaultDarkTextColour
                );
            }
        }

        /* Buttons */
        foreach (Button button in staticButtons) {
            button.Render();
        }

        /* Display detailed card if applicable */
        if (displayPlayer != null) {
            // Make screen darker
            DrawRectangle(0, 0, ScreenWidth, ScreenHeight, DarkenFilter);

            displayPlayer.DisplayDetailedCard();
            exitButton.Render();
        }
    }

    public void SetDisplayPlayer(Player? player) {
        if (player == null) {
            // Activate all the buttons
            foreach (Button button in staticButtons) {
                button.Activate();
            }
            foreach (Button button in dynamicButtons) {
                button.Activate();
            }
        } else {
            // Deactivate all the buttons
            foreach (Button button in staticButtons) {
                button.Deactivate();
            }
            foreach (Button button in dynamicButtons) {
                button.Deactivate();
            }
        }

        displayPlayer = player;
    }
}
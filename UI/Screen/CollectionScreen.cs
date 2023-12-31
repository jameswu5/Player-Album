using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.Player;
using static PlayerAlbum.Settings.CollectionScreen;

namespace PlayerAlbum;

public class CollectionScreen : Screen {

    private int page;
    private int maxPages;
    public Collection collection;
    private List<PlayerStatus> playerStatuses;
    public Club? club;

    private List<Button> playerButtons;
    private HoverButton exitButton;

    public Player? displayPlayer;
    public bool ownedFlag;

    public CollectionScreen() {
        page = 0;
        playerStatuses = new();
        playerButtons = new();
        displayPlayer = null;
        ownedFlag = false;
        InitialiseButtons();
    }

    protected override void InitialiseButtons() {
        staticButtons = new();

        // Back button
        HoverButton backButton = new HoverButton(
            0, 0, HeaderHeight, HeaderHeight,
            colour: BackButtonColour,
            hoverColour: BackButtonHoverColour,
            text: "Back",
            textColour: Helper.GetTextColour(BackButtonColour),
            fontSize: HeaderFontSize
        );
        AddButtonAction(backButton, new Action(targetScreen: Game.GameScreen.Menu));
        staticButtons.Add(backButton);

        // Direction buttons
        HoverButton left = new HoverButton(
            0, DirectionButtonPadding, DirectionButtonWidth, DirectionButtonHeight,
            colour: DirectionButtonColour, text: "<", fontSize: DirectionButtonFontSize
        );
        AddButtonAction(left, new Action(pageShift: -1));
        staticButtons.Add(left);
        
        HoverButton right = new HoverButton(
            ScreenWidth - DirectionButtonWidth, DirectionButtonPadding, DirectionButtonWidth, DirectionButtonHeight,
            colour: DirectionButtonColour, text: ">", fontSize: DirectionButtonFontSize
        );
        AddButtonAction(right, new Action(pageShift: 1));
        staticButtons.Add(right);

        // Exit button
        exitButton = new HoverButton(
            DCardWidthOffset + DCardWidth - ExitPadding - ExitButtonSize,
            DCardHeightOffset + ExitPadding,
            ExitButtonSize, ExitButtonSize, colour: ExitButtonColour, hoverColour: ExitButtonHoverColour, text: "x"
        );
        AddButtonAction(exitButton, new Action(player: null));

        // Owned toggle button
        HoverButton ownedButton = new HoverButton(
            ScreenWidth - OwnedButtonWidth, 0, OwnedButtonWidth, HeaderHeight,
            colour: OwnedButtonColour, text: "Owned", textColour: Helper.GetTextColour(OwnedButtonColour), fontSize: HeaderFontSize
        );
        AddButtonAction(ownedButton, new Action(ownedToggle: true));
        staticButtons.Add(ownedButton);
    }

    public void SetClub(Club? club, Dictionary<int, int> save, bool ownedOnly = false) {
        this.club = club;

        if (club == null) {
            List<Player> players = Database.GetPlayers($"""SELECT * FROM Player WHERE League = "{collection.name}" ORDER BY Overall DESC""");
            if (ownedOnly) {
                List<Player> filteredPlayers = new();
                foreach (Player player in players) {
                    if (save.ContainsKey(player.ID)) {
                        filteredPlayers.Add(player);
                    }
                }
                playerStatuses = Helper.GetPlayerStatuses(filteredPlayers, save);
            } else {
                playerStatuses = Helper.GetPlayerStatuses(players, save);
            }
        } else {
            Club cur = (Club)club;
            List<Player> players = Database.GetPlayers($"""SELECT * FROM Player WHERE Club = "{cur.name}" AND League = "{collection.name}" ORDER BY Overall DESC""");
            if (ownedOnly) {
                List<Player> filteredPlayers = new();
                foreach (Player player in players) {
                    if (save.ContainsKey(player.ID)) {
                        filteredPlayers.Add(player);
                    }
                }
                playerStatuses = Helper.GetPlayerStatuses(filteredPlayers, save);
            } else {
                playerStatuses = Helper.GetPlayerStatuses(players, save);
            }
        }

        ownedFlag = ownedOnly;
        maxPages = (playerStatuses.Count - 1) / (Rows * Columns);
        ResetPage();
    }

    public void ShiftPage(int shift) {
        if (shift > 0) {
            page = Math.Min(page + shift, maxPages);
        }

        if (shift < 0) {
            page = Math.Max(page + shift, 0);
        }

        playerButtons = GetPlayerButtons();
        SetDisplayPlayer(null);
    }

    public void ResetPage() => ShiftPage(-page);

    public List<Button> GetPlayerButtons() {
        List<Button> res = new();
        int indexOffset = page * Rows * Columns;
        for (int i = 0; i < Rows; i++) {
            for (int j = 0; j < Columns; j++) {
                int index = i * Columns + j + indexOffset;
                if (index >= playerStatuses.Count) {
                    break;
                }

                // Only make the button if the player is collected.
                if (playerStatuses[index].isCollected == false) {
                    continue;
                }

                int posX = HorizontalPadding + (CardWidth + CardPadding) * j;
                int posY = HeaderHeight + VerticalPadding + (CardHeight + VerticalPadding) * i;

                BorderButton button = new(posX, posY, CardWidth, CardHeight);
                AddButtonAction(button, new Action(player: playerStatuses[index].player));
                res.Add(button);
            }
        }
        return res;
    }

    public override void Display() {
        /* Header */
        Color headerColour = club == null ? HeaderColour : ((Club)club).colour;
        DrawRectangle(0, 0, ScreenWidth, HeaderHeight, headerColour);

        // Club name
        string text = club == null ? "All Players" : ((Club)club).name;
        (int x, int y) headerPos = Helper.GetTextPositions(text, ScreenWidth, HeaderHeight, HeaderFontSize);
        DrawText(text, headerPos.x, headerPos.y, HeaderFontSize, Helper.GetTextColour(headerColour));

        /* Players */
        foreach (Button button in playerButtons) {
            button.Render();
        }

        int indexOffset = page * Rows * Columns;
        for (int i = 0; i < Rows; i++) {
            for (int j = 0; j < Columns; j++) {
                int index = i * Columns + j + indexOffset;
                if (index >= playerStatuses.Count) {
                    break;
                }

                int posX = HorizontalPadding + (CardWidth + CardPadding) * j;
                int posY = HeaderHeight + VerticalPadding + (CardHeight + VerticalPadding) * i;
                playerStatuses[index].player.DisplayCard(posX, posY, playerStatuses[index].isCollected);
            }
        }

        /* Buttons */
        foreach (Button button in staticButtons) {
            button.Render();
        }

        /* Page number */
        string pageText = $"Page {page + 1} of {maxPages + 1}";
        (int x, int y) pagePos = Helper.GetTextPositions(pageText, ScreenWidth, VerticalPadding, PageNumberFontSize);
        DrawText(pageText, pagePos.x, ScreenHeight - VerticalPadding + pagePos.y, PageNumberFontSize, DefaultDarkTextColour);

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
            foreach (Button button in playerButtons) {
                button.Activate();
            }
        } else {
            // Deactivate all the buttons
            foreach (Button button in staticButtons) {
                button.Deactivate();
            }
            foreach (Button button in playerButtons) {
                button.Deactivate();
            }
        }

        displayPlayer = player;
    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.HomeScreen;

namespace PlayerAlbum;

public class HomeScreen : Screen {

    private List<Collection> collections;

    private int heightOffset;

    private const string TitleText = "Select Collection:";

    public HomeScreen(List<Collection> collections) {
        this.collections = collections;

        // UI Offsets
        int heightNeeded = FontSize + collections.Count * (ButtonHeight + Padding);
        heightOffset = (ScreenHeight - heightNeeded) >> 1;
        InitialiseButtons();
    }

    protected override void InitialiseButtons() {
        staticButtons = new();

        int posX = (ScreenWidth - ButtonWidth) >> 1;
        for (int i = 0; i < collections.Count; i++) {
            int posY = heightOffset + FontSize + (i + 1) * Padding + i * ButtonHeight;
            HoverButton button = new(posX, posY, ButtonWidth, ButtonHeight, name: collections[i].name, text: collections[i].name, fontSize: ButtonFontSize);
            Action action = new(targetScreen: Game.GameScreen.Menu, debugText: collections[i].name, collection: collections[i]);
            AddButtonAction(button, action);
            staticButtons.Add(button);
        }
    }

    public override void Display() {
        // Title
        int titleWidthOffset = (ScreenWidth - MeasureText(TitleText, FontSize)) >> 1;
        DrawText(TitleText, titleWidthOffset, heightOffset, FontSize, Color.BLACK);

        // Buttons
        foreach (Button button in staticButtons) {
            button.Render();
        }
    }
}
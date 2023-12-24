using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static PlayerAlbum.Settings;
using static PlayerAlbum.Settings.HomeScreen;

namespace PlayerAlbum;

public class HomeScreen : Screen {

    private List<Collection> collections;
    private List<Button> buttons;

    private int heightOffset;

    private const string TitleText = "Select Collection:";

    public HomeScreen(List<Collection> collections) {
        this.collections = collections;

        // UI Offsets
        int heightNeeded = FontSize + collections.Count * (ButtonHeight + Padding);
        heightOffset = (ScreenHeight - heightNeeded) >> 1;
        buttons = InitialiseButtons();
    }

    private List<Button> InitialiseButtons() {
        List<Button> res = new();

        int posX = (ScreenWidth - ButtonWidth) >> 1;
        for (int i = 0; i < collections.Count; i++) {
            int posY = heightOffset + FontSize + (i + 1) * Padding + i * ButtonHeight;
            TextButton button = new(posX, posY, ButtonWidth, ButtonHeight, name: collections[i].name, text: collections[i].name, fontSize: ButtonFontSize);
            Action action = new(targetScreen: Game.GameScreen.Menu, debugText: collections[i].name, collection: collections[i]);
            AddButtonAction(button, action);
            res.Add(button);
        }

        return res;
    }

    public override void Display() {
        // Title
        int titleWidthOffset = (ScreenWidth - MeasureText(TitleText, FontSize)) >> 1;
        DrawText(TitleText, titleWidthOffset, heightOffset, FontSize, Color.BLACK);

        // Buttons
        foreach (Button button in buttons) {
            button.Render();
        }
    }
}
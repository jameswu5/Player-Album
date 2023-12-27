using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public abstract class Button {

    public event System.Action OnClick;

    protected readonly string name;
    protected readonly string text;
    protected readonly Color textColour;
    protected readonly int fontSize;

    protected bool activated; // Deactivated buttons still display, but cannot be clicked or hovered

    public Button(string? name = null, string? text = null, Color? textColour = null, int? fontSize = null) {
        this.name = name ?? "";
        this.text = text ?? "";
        this.textColour = textColour ?? Settings.DefaultDarkTextColour;
        this.fontSize = fontSize ?? Settings.MediumFontSize;
        activated = true;
    }

    public void Render() {
        if (!activated) {
            Display();
            return;
        }

        if (IsHovered(GetMouseX(), GetMouseY())) {
            HoverDisplay();
            if (IsMouseButtonPressed(0)) {
                Click();
            }
        } else {
            Display();
        }
    }

    protected abstract bool IsHovered(float x, float y);

    protected abstract void Display();

    // protected void DisplayText(int centreX, int centreY) {
    //     int posX = centreX - MeasureText(text, fontSize) / 2;
    //     int posY = centreY - fontSize / 2;
    //     DrawText(text, posX, posY, fontSize, textColour);
    // }

    protected abstract void DisplayText();

    protected virtual void HoverDisplay() => Display();

    protected virtual void Click() => OnClick.Invoke();

    public void Activate() => activated = true;

    public void Deactivate() => activated = false;
}
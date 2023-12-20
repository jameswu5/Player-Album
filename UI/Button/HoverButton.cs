using System;
using System.Runtime.InteropServices;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// A button that changes colour when you hover over it.
/// </summary>
public class HoverButton : Button {
    private Color colour;
    private Color hoverColour;
    private Color textColour;
    private readonly string text;
    private readonly int fontSize;

    public HoverButton(int posX, int posY, int width, int height, Color? colour = null, Color? hoverColour = null, Color? textColour = null, string name = "", string text = "", int fontSize = -1) : base(posX, posY, width, height, name) {
        this.colour = colour ?? Settings.ButtonColour;
        this.hoverColour = hoverColour ?? Settings.ButtonHoverColour;
        this.textColour = textColour ?? Settings.ButtonTextColour;
        this.fontSize = fontSize == -1 ? Settings.MediumFontSize : fontSize;
        this.text = text;
    }

    protected override void Display()
    {
        DrawRectangle(posX, posY, width, height, colour);
        DisplayText();
    }

    protected override void HoverDisplay()
    {
        DrawRectangle(posX, posY, width, height, hoverColour);
        DisplayText();
    }

    private void DisplayText() {
        if (text.Length == 0) return;

        int textWidth = MeasureText(text, fontSize);
        int x = (width - textWidth) / 2 + posX;
        int y = (height - fontSize) / 2 + posY;

        DrawText(text, x, y, fontSize, textColour);
    }
}
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

/// <summary>
/// A button that displays text and changes appearance when hovered.
/// </summary>
public class HoverButton : RectangularButton {
    private Color colour;
    private Color hoverColour;

    public HoverButton(int posX, int posY, int width, int height, Color? colour = null, Color? hoverColour = null, Color? textColour = null, string? name = null, string? text = null, int? fontSize = null) : base(posX, posY, width, height, name, text, textColour, fontSize) {
        this.colour = colour ?? Settings.ButtonColour;
        this.hoverColour = hoverColour ?? Settings.ButtonHoverColour;
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
}
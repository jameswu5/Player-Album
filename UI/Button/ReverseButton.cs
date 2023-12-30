using System;

namespace PlayerAlbum;

/// <summary>
/// An invisible button which is clicked when the mouse is outside the specified rectangle.
/// </summary>
public class ReverseButton : Button {

    protected readonly int posX;
    protected readonly int posY;
    protected readonly int height;
    protected readonly int width;
    protected readonly string name;

    public ReverseButton(int posX, int posY, int width, int height, string? name = null) : base(name) {
        this.posX = posX;
        this.posY = posY;
        this.height = height;
        this.width = width;
    }

    protected override bool IsHovered(float x, float y) => !(x >= posX && x <= posX + width && y >= posY && y <= posY + height);

    protected override void Display() {}

    protected override void DisplayText() {}
}
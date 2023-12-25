using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace PlayerAlbum;

public abstract class Button {

    public event System.Action OnClick;

    protected readonly int posX;
    protected readonly int posY;
    protected readonly int height;
    protected readonly int width;
    protected readonly string name;

    public Button(int posX, int posY, int width, int height, string? name = null) {
        this.posX = posX;
        this.posY = posY;
        this.height = height;
        this.width = width;
        this.name = name ?? "";
    }

    public void Render() {
        if (IsHovered(GetMouseX(), GetMouseY())) {
            HoverDisplay();
            if (IsMouseButtonPressed(0)) {
                Click();
            }
        } else {
            Display();
        }
    }

    protected bool IsHovered(float x, float y) => x >= posX && x <= posX + width && y >= posY && y <= posY + height;

    /* These methods are supposed to be overridden.
     * Implement how the button should be displayed,
     * and by default hover display is the same.
     * Also implement when it's clicked, a basic
     * functionality is already provided.
     */

    protected abstract void Display();

    protected virtual void HoverDisplay() => Display();

    protected virtual void Click() => OnClick.Invoke();
}
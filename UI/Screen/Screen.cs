using System;

namespace PlayerAlbum;

public abstract class Screen {

    public System.Action<PlayerAlbum.Action> clickAction;

    public abstract void Display();

    protected void AddButtonAction(Button button, Action action) {
        button.OnClick += () => clickAction(action);
    }

    // I would much prefer to have one general function that does this, but
    // apparently it's not possible
    protected void AddButtonAction(HoverButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }
}
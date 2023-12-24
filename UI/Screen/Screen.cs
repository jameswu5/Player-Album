using System;

namespace PlayerAlbum;

public abstract class Screen {

    public System.Action<PlayerAlbum.Action> clickAction;

    public abstract void Display();

    protected void AddButtonAction(Button button, Action action) {
        button.OnClick += () => clickAction(action);
    }

    protected void AddButtonAction(TextButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }

    protected void AddButtonAction(ImageButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }
}
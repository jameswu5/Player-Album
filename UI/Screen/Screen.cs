using System;

namespace PlayerAlbum;

public abstract class Screen {

    public System.Action<PlayerAlbum.Action> clickAction;
    protected List<Button> buttons;

    public abstract void Display();

    protected abstract List<Button> InitialiseButtons();

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
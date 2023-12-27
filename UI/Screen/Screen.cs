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

    protected void AddButtonAction(RectangularButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }

    protected void AddButtonAction(HoverButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }

    protected void AddButtonAction(BorderButton button, Action action) {
        button.OnClick += () => clickAction(action);
    }
}
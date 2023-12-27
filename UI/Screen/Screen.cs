using System;

namespace PlayerAlbum;

public abstract class Screen {

    public System.Action<PlayerAlbum.Action> clickAction;
    protected List<Button> staticButtons;

    public abstract void Display();

    protected abstract void InitialiseButtons();

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
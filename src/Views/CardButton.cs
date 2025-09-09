using Godot;
using UnoNoMercy.Entities.Card;

namespace UnoNoMercy.Views;

public partial class CardButton : Button
{
    public Card Card
    {
        get => _card;
        set
        {
            _card = value;
            SetText(_card.ToString());
        }
    }

    public bool IsSelected { get; private set; }
    private Card _card;
    private Vector2 _originalPosition;

    public CardButton()
    {
        ToggleMode = true;
    }

    public override void _Toggled(bool toggledOn)
    {
        IsSelected = toggledOn;
        if (toggledOn)
            MoveUp();
        else
            MoveBackDown();
    }

    private void MoveBackDown()
    {
        Position = _originalPosition;
    }

    private void MoveUp()
    {
        _originalPosition = Position;
        const float offset = 10;
        Position = _originalPosition with { Y = _originalPosition.Y - offset };
    }
}
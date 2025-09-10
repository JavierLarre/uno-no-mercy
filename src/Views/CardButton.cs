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

    public override void _Pressed()
    {
        IsSelected = !IsSelected;
        if (IsSelected)
            MoveUp();
        else
            MoveBackDown();
    }

    public void MoveBackDown()
    {
        Position = _originalPosition;
        IsSelected = false;
    }

    private void MoveUp()
    {
        const float offset = 10;
        _originalPosition = Position;
        Vector2 newPosition = _originalPosition;
        newPosition.Y = _originalPosition.Y - offset;
        Position = newPosition;
    }
}
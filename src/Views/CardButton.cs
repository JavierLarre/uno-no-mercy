using Godot;
using UnoNoMercy.Entities.Card;
using UnoNoMercy.Models;

public partial class CardButton : Button
{
    public Card Card { get; private set; }
    
    public CardButton(Card card)
    {
        Card = card;
        SetText(card.ToString());
        ToggleMode = true;
    }

    public override void _Toggled(bool toggledOn)
    {
        if (toggledOn)
        {
            Position = Position with { Y = Position.Y - 10 };
        }
        else
            Position = Position with { Y = Position.Y + 10 };
    }
}

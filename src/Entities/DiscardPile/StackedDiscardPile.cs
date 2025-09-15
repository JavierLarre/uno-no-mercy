using System.Collections.Generic;
using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Entities.DiscardPile;

public class StackedDiscardPile: DiscardPile
{
    public Card TopCard
    {
        get => _cards.Peek();
        set => _cards.Push(value);
    }

    private Stack<Card> _cards;

    public StackedDiscardPile(Card firstCard)
    {
        _cards = new Stack<Card>();
        _cards.Push(firstCard);
    }

    public static StackedDiscardPile GetPileWithCard() =>
        new(Card.GetGreenEight());
}
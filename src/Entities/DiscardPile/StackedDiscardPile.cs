using System.Collections.Generic;
using UnoNoMercy.Models;

namespace UnoNoMercy.Entities.DiscardPile;

public class StackedDiscardPile
{
    public Card.Card TopCard
    {
        get => _cards.Peek();
        set => _cards.Push(value);
    }

    private Stack<Card.Card> _cards;

    public StackedDiscardPile(Card.Card firstCard)
    {
        _cards = new Stack<Card.Card>();
        _cards.Push(firstCard);
    }

    public static StackedDiscardPile GetPileWithCard() =>
        new(Card.Card.GetGreenEight());
}
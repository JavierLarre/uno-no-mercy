using System.Collections.Generic;

namespace UnoNoMercy.Models;

public class DiscardPile
{
    public Card TopCard
    {
        get => _cards.Peek();
        set => _cards.Push(value);
    }

    private Stack<Card> _cards;

    public DiscardPile(Card firstCard)
    {
        _cards = new Stack<Card>();
        _cards.Push(firstCard);
    }

    public static DiscardPile GetPileWithCard() =>
        new(Card.GetGreenEight());
}
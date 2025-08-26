using System.Collections.Generic;

namespace UnoNoMercy.Cards;

public class DiscardPile
{
    private Stack<Card> _cards = new();

    public void Add(Card card) => _cards.Push(card);
    public Card Peek() => _cards.Peek();
}
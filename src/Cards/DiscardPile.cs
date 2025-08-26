using System.Collections.Generic;

namespace UnoNoMercy.Cards;

public class DiscardPile
{
    private Stack<Card> _cards;

    public DiscardPile(Card firstCard)
    {
        _cards = new Stack<Card>();
        _cards.Push(firstCard);
    }

    public void Add(Card card) => _cards.Push(card);
    public Card Peek() => _cards.Peek();
}
using System.Collections.Generic;

namespace UnoNoMercy.Cards;

public class Deck
{
    private Stack<Card> _cards;

    public Deck(IEnumerable<Card> cards)
    {
        _cards = new Stack<Card>(cards);
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
            throw new DeckIsEmptyException();
        return _cards.Pop();
    }
}
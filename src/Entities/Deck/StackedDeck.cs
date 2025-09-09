using System.Collections.Generic;
using UnoNoMercy.Cards;
using UnoNoMercy.Entities.Card;

namespace UnoNoMercy.Models;

public class StackedDeck
{
    private Stack<Card> _cards;

    public StackedDeck(IEnumerable<Card> cards)
    {
        _cards = new Stack<Card>(cards);
    }
    
    public static StackedDeck GetDeckWithCards(int cards) => 
        new(Card.GetGreenEights(cards));

    public Card Draw()
    {
        if (_cards.Count == 0)
            throw new DeckIsEmptyException();
        return _cards.Pop();
    }
}
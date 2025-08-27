using System.Collections.Generic;
using UnoNoMercy.Cards;

namespace UnoNoMercy.Models;

public class Deck
{
    private Stack<Card> _cards;

    public Deck(IEnumerable<Card> cards)
    {
        _cards = new Stack<Card>(cards);
    }
    
    public static Deck GetDeckWithCards(int cards) => 
        new(Card.GetGreenEights(cards));

    public Card Draw()
    {
        if (_cards.Count == 0)
            throw new DeckIsEmptyException();
        return _cards.Pop();
    }
}
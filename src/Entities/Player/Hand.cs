using System.Collections.Generic;
using System.Linq;
using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Models;

public class Hand
{
    public Card[] Cards => _cards.ToArray();
    private List<Card> _cards;

    public Hand(IEnumerable<Card> cards)
    {
        _cards = cards.ToList();
    }

    public static Hand GetHandWithCards(int handSize)
    {
        return new Hand(Card.GetGreenEights(handSize));
    }

    public void AddCard(Card card) => _cards.Add(card);

    public override string ToString()
    {
        return $"[{string.Join(", ", _cards)}]";
    }
}
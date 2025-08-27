using System.Collections.Generic;
using System.Linq;

namespace UnoNoMercy.Models;

public class Player
{
    public Card[] Hand => _hand.ToArray();
    private List<Card> _hand;

    public Player(IEnumerable<Card> cards)
    {
        _hand = cards.ToList();
    }
    

    public void AddCardToHand(Card card) => _hand.Add(card);

    public static Player GetPlayerWithCards(int handSize)
    {
        return new Player(Card.GetGreenEights(handSize));
    }
}
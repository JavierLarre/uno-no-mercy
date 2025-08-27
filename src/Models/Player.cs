using System.Collections.Generic;
using System.Linq;

namespace UnoNoMercy.Models;

public class Player
{
    public IList<Card> Hand { get; private set; }

    public Player(IEnumerable<Card> cards)
    {
        Hand = cards.ToList();
    }
    
    public static Player GetPlayerWithCards(int handSize)
    {
        return new Player(Card.GetGreenEights(handSize));
    }
}
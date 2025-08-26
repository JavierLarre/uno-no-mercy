using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnoNoMercy.Cards;

namespace UnoNoMercy;

public class Player
{
    public Card[] Hand { get; private set; }

    public Player(IEnumerable<Card> cards)
    {
        Hand = cards.ToArray();
    }
}
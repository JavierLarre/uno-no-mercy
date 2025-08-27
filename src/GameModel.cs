using System.Collections.Generic;
using UnoNoMercy.Cards;

namespace UnoNoMercy;

public class GameModel
{
    public IList<Player> Players { get; set; }
    public TurnDirection TurnDirection { get; set; }
    public Deck Deck { get; set; }
    public DiscardPile DiscardPile { get; set; }
}
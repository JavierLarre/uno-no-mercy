using System.Collections.Generic;
using UnoNoMercy.Cards;

namespace UnoNoMercy.Models;

public class GameModel
{
    public IList<Hand> Players { get; set; }
    public TurnDirection TurnDirection { get; set; }
    public Deck Deck { get; set; }
    public DiscardPile DiscardPile { get; set; }
    public Hand PlayerInTurn { get; set; }
}
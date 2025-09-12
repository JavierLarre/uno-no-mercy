using System.Collections.Generic;
using UnoNoMercy.Cards;
using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;

namespace UnoNoMercy.Models;

public class GameModel
{
    public IList<Hand> Players { get; set; }
    public TurnDirection TurnDirection { get; set; }
    public StackedDeck StackedDeck { get; init; }
    public StackedDiscardPile StackedDiscardPile { get; init; }
    public Hand PlayerInTurn { get; set; }
}
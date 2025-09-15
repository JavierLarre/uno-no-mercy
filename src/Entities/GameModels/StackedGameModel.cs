using System.Collections.Generic;
using UnoNoMercy.Cards;
using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Entities.Player;

namespace UnoNoMercy.Models;

public class StackedGameModel: GameModel
{
    public IList<Hand> PlayerList { get; set; }
    public TurnDirection TurnDirection { get; set; }
    public Deck Deck { get; init; }
    public DiscardPile DiscardPile { get; init; }
    public Hand PlayerInTurn { get; set; }

    public Players Players { get; set; }
}
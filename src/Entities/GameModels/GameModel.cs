using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Entities.Player;

namespace UnoNoMercy.Models;

public interface GameModel
{
    Players Players { get; }
    Deck Deck { get; }
    DiscardPile DiscardPile { get; }
}
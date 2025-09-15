using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Entities.Player;

namespace UnoNoMercy.Models;

public class MockGameModel: GameModel
{
    public Players Players { get; init; }
    public Deck Deck { get; init; }
    public DiscardPile DiscardPile { get; init; }
}
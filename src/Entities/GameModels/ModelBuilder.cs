using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.Deck;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Entities.Player;

namespace UnoNoMercy.Models;

public class ModelBuilder
{
    public Hand[] Players { get; set; }
    public GameModel GetMockModel()
    {
        return new MockGameModel
        {
            DiscardPile = new MockDiscardPile(),
            Players = GetPlayers(),
            Deck = new MockDeck()
        };
    }

    private Players GetPlayers()
    {
        return new PlayerList
        {
            PlayersArray = Players, 
            CurrentPlayerHand = Players[0],
            TurnDirection = TurnDirection.Right
        };
    }
}
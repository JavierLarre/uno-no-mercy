using System.Collections.Generic;
using System.Linq;

namespace UnoNoMercy.Models.Players;

public class Players: IPlayers
{
    private TurnDirection _turnDirection = TurnDirection.Right;
    private Queue<Hand> _players;

    public Players(Hand[] players)
    {
        _players = new Queue<Hand>(players);
    }

    public Hand CurrentPlayer => _players.Peek();
    public Hand NextPlayer => GetNextPlayer();

    public TurnDirection TurnDirection
    {
        get => _turnDirection;
        set => SetTurnDirection(value);
    }

    public void PassTurn()
    {
        Hand previousPlayer = _players.Dequeue();
        _players.Enqueue(previousPlayer);
    }

    private void SetTurnDirection(TurnDirection newTurnDirection)
    {
        if (_turnDirection != newTurnDirection)
            ReverseOrder();
        _turnDirection = newTurnDirection;
    }

    private Hand GetNextPlayer()
    {
        if (_players.Count == 1)
            return CurrentPlayer;
        return _players.ElementAt(1);
    }
    private void ReverseOrder()
    {
        _players = new Queue<Hand>(_players.Reverse());
    }
}
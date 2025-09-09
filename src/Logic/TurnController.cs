using System;
using System.Linq;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public class TurnController
{
    private GameModel _model;
    private Hand[] _players;

    public TurnController(GameModel model)
    {
        _model = model;
        _players = _model.Players.ToArray();
    }

    public void PassTurn()
    {
        _model.PlayerInTurn = GetNextPlayer();
    }

    public Hand GetNextPlayer()
    {
        int nextPlayerIndex = GetNextPlayerIndex();
        return _players[nextPlayerIndex];
    }

    private int GetNextPlayerIndex()
    {
        if (_model.TurnDirection is TurnDirection.Right)
            return GetRightPlayerIndex();
        return GetLeftPlayerIndex();
    }

    private int GetLeftPlayerIndex()
    {
        int currentPlayerIndex = GetPlayerInTurnIndex();
        return int.Max(currentPlayerIndex - 1, 0);
    }

    private int GetRightPlayerIndex()
    {
        int currentPlayerIndex = GetPlayerInTurnIndex();
        return (currentPlayerIndex + 1) % _players.Length;
    }

    private int GetPlayerInTurnIndex() => 
        Array.IndexOf(_players, _model.PlayerInTurn);
}
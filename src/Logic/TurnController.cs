using System;
using System.Linq;
using UnoNoMercy.Entities.Player;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public class TurnController
{
    private Players _players;

    public TurnController(GameModel model)
    {
        _players = model.Players;
    }

    public void PassTurn()
    {
        _players.CurrentPlayerHand = GetNextPlayer();
    }

    public Hand GetNextPlayer()
    {
        int nextPlayerIndex = GetNextPlayerIndex();
        return _players.PlayersArray[nextPlayerIndex];
    }

    private int GetNextPlayerIndex()
    {
        if (_players.TurnDirection is TurnDirection.Right)
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
        int playerAmount = _players.PlayersArray.Length;
        return (currentPlayerIndex + 1) % playerAmount;
    }

    private int GetPlayerInTurnIndex() => 
        Array.IndexOf(_players.PlayersArray, _players.CurrentPlayerHand);
}
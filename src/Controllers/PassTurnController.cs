using System.Linq;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public class PassTurnController
{
    private Hand[] _players;
    private Hand _playerInTurn;

    public PassTurnController(GameModel model)
    {
        _players = model.Players.ToArray();
        _playerInTurn = model.PlayerInTurn;
    }

    public void PassTurn()
    {
        
    }
}
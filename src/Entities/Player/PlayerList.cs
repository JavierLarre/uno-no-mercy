using System.Collections.Generic;
using System.Linq;
using UnoNoMercy.Models;

namespace UnoNoMercy.Entities.Player;

public class PlayerList: Players
{
    public Hand CurrentPlayerHand { get; set; }
    public Hand[] PlayersArray { get; set; }
    public TurnDirection TurnDirection { get; set; }
}
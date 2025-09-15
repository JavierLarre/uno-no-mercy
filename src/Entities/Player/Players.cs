using UnoNoMercy.Models;

namespace UnoNoMercy.Entities.Player;

public interface Players
{
    Hand CurrentPlayerHand { get; set; }
    Hand[] PlayersArray { get; }
    TurnDirection TurnDirection { get; set; }
}
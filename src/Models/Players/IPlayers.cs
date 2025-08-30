namespace UnoNoMercy.Models.Players;

public interface IPlayers
{
    Hand CurrentPlayer { get; }
    Hand NextPlayer { get; }
    TurnDirection TurnDirection { get; set; }
    void PassTurn();
}
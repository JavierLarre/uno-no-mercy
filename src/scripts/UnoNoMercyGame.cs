using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Models;
using UnoNoMercy.Views;

namespace UnoNoMercy.scripts;

public partial class UnoNoMercyGame : Node
{
    [Export] 
    public CardColor InitialColor { get; set; } = CardColor.Green;

    [Export] 
    public CardValue InitialValue { get; set; } = CardValue.Eight;

    [Export] 
    public TurnDirection InitialDirection { get; set; } = TurnDirection.Right;
    
    public override void _Ready()
    {
        Card initialCard = new Card
        {
            Color = InitialColor, Value = InitialValue
        };
        Hand hand = Hand.GetHandWithCards(7);
        GameModel model = new GameModel
        {
            Deck = Deck.GetDeckWithCards(100),
            DiscardPile = new DiscardPile(initialCard),
            PlayerInTurn = hand,
            Players = [hand],
            TurnDirection = TurnDirection.Right
        };
        Ui ui = GetChild<Ui>(0);
        GameController controller = new GameController(model);
        controller.Start(ui);
    }
}
using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Entities.Card;
using UnoNoMercy.Entities.DiscardPile;
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
            StackedDeck = StackedDeck.GetDeckWithCards(100),
            StackedDiscardPile = new StackedDiscardPile(initialCard),
            PlayerInTurn = hand,
            Players = [hand],
            TurnDirection = TurnDirection.Right
        };
        Ui ui = GetChild<Ui>(0);
        CardController controller = new CardController(model);
        controller.Start(ui);
    }
}
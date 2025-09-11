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

    private NodePath _uiPath = new("UI");
    
    public override void _Ready()
    {
        var model = GetModel();
        var ui = GetUi();
        CardController controller = new CardController(model);
        controller.Start(ui);
    }

    private GameModel GetModel()
    {
        var initialCard = GetInitialCard();
        Hand hand = Hand.GetHandWithCards(7);
        GameModel model = new GameModel
        {
            StackedDeck = StackedDeck.GetDeckWithCards(100),
            StackedDiscardPile = new StackedDiscardPile(initialCard),
            PlayerInTurn = hand,
            Players = [hand],
            TurnDirection = TurnDirection.Right
        };
        return model;
    }

    private Ui GetUi()
    {
        Ui ui = GetNode<Ui>(_uiPath);
        return ui;
    }

    private Card GetInitialCard()
    {
        Card initialCard = new Card
        {
            Color = InitialColor, Value = InitialValue
        };
        return initialCard;
    }
}
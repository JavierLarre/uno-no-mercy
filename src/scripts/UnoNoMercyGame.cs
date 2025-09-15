using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Controllers;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.Deck;
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

    private StackedGameModel GetModel()
    {
        var initialCard = GetInitialCard();
        Hand hand = Hand.GetHandWithCards(7);
        StackedGameModel model = new StackedGameModel
        {
            Deck = StackedDeck.GetDeckWithCards(100),
            DiscardPile = new StackedDiscardPile(initialCard),
            PlayerInTurn = hand,
            PlayerList = [hand],
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
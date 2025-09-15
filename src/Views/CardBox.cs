using System.Collections.Generic;
using Godot;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Models;

namespace UnoNoMercy.Views;

public partial class CardBox : HBoxContainer
{
    public Card SelectedCard { get; private set; }
    private GameModel _model;
    private List<CardButton> _buttons = [];

    public void SetModel(GameModel model) => _model = model;

    public void Update()
    {
        IEnumerable<Card> cards = _model.Players.CurrentPlayerHand.Cards;
        foreach (Card card in cards) 
            AddCardToBox(card);
    }

    private void AddCardToBox(Card card)
    {
        CardButton cardButton = new CardButton { Card = card };
        cardButton.Pressed += OnToggledCard;

        _buttons.Add(cardButton);
        AddChild(cardButton);
        return;

        void OnToggledCard()
        {
            SelectedCard = cardButton.Card;
            DeselectCards(cardButton);
        }
    }

    private void DeselectCards(CardButton selectedCard)
    {
        foreach (CardButton button in _buttons)
        {
            if (button != selectedCard && button.IsSelected)
                button.MoveBackDown();
        }
    }
}
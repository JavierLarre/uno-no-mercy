using System.Collections.Generic;
using Godot;
using UnoNoMercy.Entities.Card;
using UnoNoMercy.Models;

namespace UnoNoMercy.Views;

public partial class CardBox : HBoxContainer
{
    private GameModel _model;

    public void SetModel(GameModel model) => _model = model;

    public void Update()
    {
        IEnumerable<Card> cards = _model.PlayerInTurn.Cards;
        foreach (Card card in cards) 
            AddCardToBox(card);
    }

    private void AddCardToBox(Card card)
    {
        CardButton cardButton = new CardButton(card);
        AddChild(cardButton);
    }
}
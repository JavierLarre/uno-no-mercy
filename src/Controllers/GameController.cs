using System;
using System.Linq;
using UnoNoMercy.Cards;
using UnoNoMercy.Cards.CardEffects;
using UnoNoMercy.Controllers.CardEffects;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public class GameController
{
    private DiscardPile _discardPile;
    private GameModel _model;

    public GameController(GameModel model)
    {
        _model = model;
        _discardPile = model.DiscardPile;
    }

    public void Play(Card card)
    {
        if (!IsPlayable(card))
            throw new UnoNoMercyException();
        ApplyEffect(card);
        _discardPile.TopCard = card;
        PassTurn();
    }

    public bool IsPlayable(Card card)
    {
        var validator = new PlayableCardValidator(_model);
        return validator.IsPlayable(card);
    }

    private void ApplyEffect(Card card)
    {
        var effectFactory = new CardEffectFactory();
        ICardEffect effect = effectFactory.GetFrom(card.Value);
        effect.ApplyEffect(_model);
    }

    private void PassTurn()
    {
        Hand[] players = _model.Players.ToArray();
        int currentPlayerIndex = Array.IndexOf(players, _model.PlayerInTurn);
        int nextPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        _model.PlayerInTurn = players[nextPlayerIndex];
    }
}
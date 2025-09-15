using UnoNoMercy.Cards;
using UnoNoMercy.Controllers.CardEffects;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.DiscardPile;
using UnoNoMercy.Models;

namespace UnoNoMercy.Controllers;

public class CardController
{
    private DiscardPile _discardPile;
    private GameModel _model;
    private IView _view;

    public CardController(GameModel model)
    {
        _model = model;
        _discardPile = model.DiscardPile;
    }

    public void Start(IView view)
    {
        _view = view;
        _view.SetModel(_model);
        _view.Update();
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
        var turnController = new TurnController(_model);
        turnController.PassTurn();
    }
}
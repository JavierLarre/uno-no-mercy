namespace UnoNoMercy.Cards;

public class GameController
{
    private DiscardPile _discardPile;
    public GameController(DiscardPile discardPile)
    {
        _discardPile = discardPile;
    }

    public GameController(GameModel model)
    {
        
    }

    public void Play(Card card)
    {
        if (!IsPlayable(card))
            throw new UnoNoMercyException();
        
        _discardPile.TopCard = card;
    }

    public bool IsPlayable(Card card)
    {
        PlayableCardValidator playableCardValidator = new PlayableCardValidator(_discardPile.TopCard);
        return playableCardValidator.IsPlayable(card);
    }
}
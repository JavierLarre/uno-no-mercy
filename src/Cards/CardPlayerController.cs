namespace UnoNoMercy.Cards;

public class CardPlayerController
{
    private DiscardPile _discardPile;
    public CardPlayerController(DiscardPile discardPile)
    {
        _discardPile = discardPile;
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
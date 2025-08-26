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
        Card topCard = _discardPile.TopCard;
        return card.Color == topCard.Color || card.Value == topCard.Value;
    }
}
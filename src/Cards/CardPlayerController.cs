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

        if (topCard.Color is CardColor.Wild || card.Color is CardColor.Wild)
            return true;
        if (topCard.Color == card.Color)
            return true;
        if (topCard.Value == card.Value)
            return true;
        if (topCard.Value is CardValue.DrawTwo or CardValue.DrawFour)
            return card.Value is CardValue.DrawFour or CardValue.DrawTwo;
        return false;
    }
}
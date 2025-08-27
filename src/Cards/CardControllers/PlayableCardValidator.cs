namespace UnoNoMercy.Cards;

public class PlayableCardValidator
{
    private Card _discardCard;

    public PlayableCardValidator(GameModel model)
    {
        DiscardPile discardPile = model.DiscardPile;
        _discardCard = discardPile.TopCard;
    }
    public bool IsPlayable(Card card)
    {
        if (_discardCard.Color == card.Color)
            return true;
        if (_discardCard.Value == card.Value)
            return true;
        if (IsWild(card) || IsWild(_discardCard))
            return true;
        if (IsCardDrawEffect(_discardCard))
            return IsCardDrawEffect(card);
        return false;
    }

    private static bool IsCardDrawEffect(Card card)
    {
        return card.Value is CardValue.DrawFour or CardValue.DrawTwo;
    }

    private static bool IsWild(Card card)
    {
        return card.Color is CardColor.Wild;
    }
}
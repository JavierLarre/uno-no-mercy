namespace UnoNoMercy.Cards;

public class PlayableCardValidator
{
    private Card _discardCard;

    public PlayableCardValidator(Card card)
    {
        _discardCard = card;
    }
    public bool IsPlayable(Card card)
    {
        if (_discardCard.Color == card.Color)
            return true;
        if (_discardCard.Value == card.Value)
            return true;
        if (IsAnyWild(card))
            return true;
        if (IsCardDrawEffect(_discardCard))
            return IsCardDrawEffect(card);
        return false;
    }

    private static bool IsCardDrawEffect(Card card)
    {
        return card.Value is CardValue.DrawFour or CardValue.DrawTwo;
    }

    private bool IsAnyWild(Card card)
    {
        return _discardCard.Color is CardColor.Wild || card.Color is CardColor.Wild;
    }
}
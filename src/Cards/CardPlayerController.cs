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
        _discardPile.Add(card);
    }
}
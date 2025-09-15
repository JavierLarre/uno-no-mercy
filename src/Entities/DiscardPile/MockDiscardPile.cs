using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Entities.DiscardPile;

public class MockDiscardPile: DiscardPile
{
    public Card TopCard { get; set; } = Card.GetGreenEight();
}
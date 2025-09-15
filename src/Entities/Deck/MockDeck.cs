using UnoNoMercy.Entities.Cards;

namespace UnoNoMercy.Entities.Deck;

public class MockDeck: Deck
{
    public Card Draw() => Card.GetGreenEight();
}
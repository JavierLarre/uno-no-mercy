using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;
using UnoNoMercy.Entities.Cards;
using UnoNoMercy.Entities.Deck;

namespace UnoNoMercy.tests;

[Test]
public partial class StackedDeckTest: Node
{
    private Card _cardInDeck;
    private StackedDeck _deck;

    [BeforeEach]
    private void InitializeDeck()
    {
        _cardInDeck = Card.GetGreenEight();
        _deck = new StackedDeck([_cardInDeck]);
    }
    
    [Test]
    private void DeckDrawsCardTest()
    {
        Card drawnCard = _deck.Draw();
        
        Assert.AreEqual(_cardInDeck, drawnCard);
    }

    [Test]
    private void DeckDrawsTwoCardsTest()
    {
        Card blueFour = Card.GetBlueFour();
        _deck = new StackedDeck([_cardInDeck, blueFour]);
        
        AssertDrawCardIs(blueFour);
        AssertDrawCardIs(_cardInDeck);
    }

    [Test]
    private void DeckDrawsTooManyCardsTest()
    {
        _deck.Draw();
        Assert.Throws<DeckIsEmptyException>(() => _deck.Draw());
    }

    private void AssertDrawCardIs(Card card)
    {
        Assert.AreEqual(card, _deck.Draw());
    }
}
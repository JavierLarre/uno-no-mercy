using GD_NET_ScOUT;
using Godot;
using UnoNoMercy.Cards;

namespace UnoNoMercy.tests;

[Test]
public partial class Tester : Node
{
    private Card _greenEight;
    private DiscardPile _discardPile;
    private CardPlayerController _playController;

    [BeforeEach]
    private void InitializeAttributes()
    {
        _greenEight = GetGreenEight();
        _discardPile = new DiscardPile(GetGreenEight());
        _playController = new CardPlayerController(_discardPile);

    }
    private static Card GetGreenEight() =>
        new()
        {
            Value = CardValue.Eight,
            Color = CardColor.Green
        };

    private static Card[] GetGreenEights(int sequenceLength)
    {
        Card[] validCards = new Card[sequenceLength];
        for (int i = 0; i < sequenceLength; i++)
        {
            validCards[i] = GetGreenEight();
        }

        return validCards;
    }

    private static Card GetRedNine() =>
        new()
        {
            Color = CardColor.Red,
            Value = CardValue.Nine
        };

    [Test]
    private void DiscardPileTest()
    {
        _discardPile = new DiscardPile(_greenEight);
        AssertCardIsOnTopPile(_greenEight);
        
        _greenEight = GetGreenEight();
        _discardPile.TopCard = _greenEight;
        AssertCardIsOnTopPile(_greenEight);
        
        _greenEight = GetGreenEight();
        AssertIsNotEqualToTopCard(_greenEight);
    }

    [Test]
    private void PlayTest()
    {
        AssertPlay(_greenEight);
    }

    [Test]
    private void PlayValidSequenceTest()
    {
        const int sequenceLength = 3;
        Card[] validCards = GetGreenEights(sequenceLength);

        foreach (Card validCard in validCards) 
            AssertPlay(validCard);
    }

    [Test]
    private void IsCardPlayableTest()
    {
        Card redNine = GetRedNine();
        
        Assert.IsFalse(_playController.IsPlayable(redNine));
        Assert.IsTrue(_playController.IsPlayable(_greenEight));
    }

    [Test]
    private void TryPlayInvalidCardTest()
    {
        Card redNine = GetRedNine();
        Assert.Throws<UnoNoMercyException>(() => _playController.Play(redNine));
        AssertIsNotEqualToTopCard(redNine);
    }

    private void AssertPlay(Card card)
    {
        _playController.Play(card);
        Assert.AreEqual(_discardPile.TopCard, card);
    }

    private void AssertCardIsOnTopPile(Card card)
    {
        Assert.AreEqual(_discardPile.TopCard, card);
    }

    private void AssertIsNotEqualToTopCard(Card card)
    {
        Assert.AreNotEqual(_discardPile.TopCard, card);
    }
}